using AutoMapper;
using Business.Interfaces;
using Business.Models.Auth;
using Business.Models.User;
using Business.Validation;
using Data.Entities;
using Data.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Business.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    
    private readonly IMapper _mapper;
    
    private readonly PasswordHasher<User> _passwordHasher;
    
    private readonly IJwtService _jwtService;
    
    public AuthService(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
        _mapper = mapper;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<TokenModel> SignUpAsync(SignUpModel signUpModel)
    {
        if (await _unitOfWork.UserRepository.ExistsByEmailAsync(signUpModel.Email))
        {
            throw new NetworkException("User with this email already exists");
        }
        
        var user = _mapper.Map<User>(signUpModel);
        user.Password = _passwordHasher.HashPassword(user, signUpModel.Password);
        await _unitOfWork.UserRepository.AddAsync(user);
        return _mapper.Map<TokenModel>(_jwtService.CreateJwtToken(user.Id));
    }
    
    public async Task<TokenModel> SignInAsync(SignInModel signInModel)
    {
        var user = await _unitOfWork.UserRepository.GetByEmailAsync(signInModel.Email);
        if (user == null)
        {
            throw new NetworkException("User with this email does not exist");
        }
        
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, signInModel.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            throw new NetworkException("Invalid password");
        }
        
        return _mapper.Map<TokenModel>(_jwtService.CreateJwtToken(user.Id));
    }
    
    public async Task<UserModel> GetCurrentUserAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        return _mapper.Map<UserModel>(user);
    }
}