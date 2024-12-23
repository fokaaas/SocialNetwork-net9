using Business.Models.Auth;
using Business.Models.User;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<TokenModel> SignUpAsync(SignUpModel signUpModel);
    
    Task<TokenModel> SignInAsync(SignInModel signInModel);
}