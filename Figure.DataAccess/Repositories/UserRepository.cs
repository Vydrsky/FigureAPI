using AutoMapper;
using Figure.DataAccess.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Figure.DataAccess.Repositories;

public class UserRepository : IUserRepository{

	private readonly ApplicationDbContext _context;
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly RoleManager<IdentityRole> _roleManager;
	private readonly IMapper _mapper;
	private string secretKey = "";

	public UserRepository(UserManager<ApplicationUser> userManager, 
		RoleManager<IdentityRole> roleManager, 
		ApplicationDbContext context, 
		IMapper mapper, 
		IConfiguration configuration) {

		_userManager = userManager;
		_context = context;
		_mapper = mapper;
		_roleManager = roleManager;
		secretKey = configuration.GetValue<string>("ApiSecretKey");
	}

	public bool IsUniqueUser(string username) {
		var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == username);
		if(user == null) {
			return true;
		}
		return false;
	}

	public async Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel) {
		var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName.ToLower() == loginRequestModel.UserName.ToLower());

		bool isPasswordValid = await _userManager.CheckPasswordAsync(user,loginRequestModel.Password);

		if(user == null || isPasswordValid == false) {
			return new LoginResponseModel {
				User = null,
				Token = ""
			};
		}

		//JWT Token Generation
		var roles = await _userManager.GetRolesAsync(user);
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(secretKey);

		List<Claim> claims = new List<Claim>();
		claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
        foreach (var role in roles) {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var tokenDescriptor = new SecurityTokenDescriptor {
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.UtcNow.AddDays(7),
			SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);

		LoginResponseModel model = new LoginResponseModel {
			User = _mapper.Map<UserModel>(user),
			Token = tokenHandler.WriteToken(token)
		};
		return model;
	}

	public async Task<UserModel> Register(RegistrationRequestModel registrationRequestModel) {
		var user = new ApplicationUser {
			UserName = registrationRequestModel.UserName,
			Name = registrationRequestModel.Name
		};

		try {
			var result = await _userManager.CreateAsync(user, registrationRequestModel.Password);
			if (result.Succeeded) {
				if(!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()) {
					await _roleManager.CreateAsync(new IdentityRole("admin"));
				}
				var roles = _userManager.GetRolesAsync(user);
				await _userManager.AddToRoleAsync(user, "ADMIN");
				var userToReturn = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == registrationRequestModel.UserName);
				return _mapper.Map<UserModel>(userToReturn);
			}
		}catch(Exception e) {

		}
		return new UserModel();
	}
}