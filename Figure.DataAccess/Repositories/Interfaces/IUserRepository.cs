using Figure.DataAccess.Models;

namespace Figure.DataAccess.Repositories.Interfaces;

public interface IUserRepository {
    public bool IsUniqueUser(string username);
    public Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    public Task<UserModel> Register(RegistrationRequestModel registrationRequestModel);
}

