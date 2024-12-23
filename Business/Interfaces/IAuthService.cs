using Business.Models.Auth;

namespace Business.Interfaces;

public interface IAuthService
{
    Task<TokenModel> SignUpAsync(SignUpModel signUpModel);

    Task<TokenModel> SignInAsync(SignInModel signInModel);
}