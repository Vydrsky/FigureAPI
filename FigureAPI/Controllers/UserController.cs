using Figure.DataAccess.Models;
using Figure.DataAccess.Repositories.Interfaces;
using Figure.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Figure.API.Controllers;
[Route("api/userauth")]
[ApiController]
public class UserController : ControllerBase {
	private APIResponse _response;
    private readonly IUserRepository _userRepository;

	public UserController(IUserRepository userRepository) {
		_userRepository = userRepository;
		_response = new();
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<ActionResult<APIResponse>> Login([FromBody] LoginRequestModel requestModel) {
		try {
			var response = await _userRepository.Login(requestModel);

			if (response.User == null || string.IsNullOrEmpty(response.Token)) {
				_response.PrepForBadRequest(new List<string> { "Username or password is incorrect" });
				return BadRequest(_response);
			}
			_response.PrepForSuccess(response);
			return Ok(_response);
		}
		catch(Exception e) {
			_response.PrepForException(e);
			return _response;
		}
	}

    [HttpPost("register")]
    public async Task<ActionResult<APIResponse>> Register([FromBody] RegistrationRequestModel requestModel) {

		bool isUnique = _userRepository.IsUniqueUser(requestModel.UserName);
		if (!isUnique) {
			_response.PrepForBadRequest(new List<string> { "Username is not unique" });
			return BadRequest(_response);
		}
		var user = await _userRepository.Register(requestModel);
		if(user == null) {
            _response.PrepForBadRequest(new List<string> { "Error while registering" });
            return BadRequest(_response);
        }
        _response.PrepForSuccess(user);
        return Ok(_response);
    }
}
