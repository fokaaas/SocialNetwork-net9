using AutoMapper;
using Business.Exceptions;
using Business.Interfaces;
using Business.Models.User;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserModel> GetByIdAsync(int id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user is null) throw new NotFoundException($"User by id {id} not found");
        return _mapper.Map<UserModel>(user);
    }

    public async Task<UsersModel> GetAllAsync()
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<UsersModel>(users);
    }

    public async Task CreateFriendshipAsync(int userId, int friendId)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        var friend = await _unitOfWork.UserRepository.GetByIdAsync(friendId);
        if (friend is null) throw new NotFoundException($"Friend by id {friend} not found");
        await _unitOfWork.FriendshipRepository.AddAsync(new Friendship
        {
            SenderId = userId,
            ReceiverId = friendId
        });
        await _unitOfWork.SaveAsync();
    }

    public async Task DeleteFriendshipAsync(int userId, int friendId)
    {
        var friendship = await _unitOfWork.FriendshipRepository.GetByIdAsync(userId, friendId);
        if (friendship is null) throw new NotFoundException("Friendship not found");
        _unitOfWork.FriendshipRepository.Delete(friendship);
        await _unitOfWork.SaveAsync();
    }

    public async Task<UserFriendshipsModel> GetFriendsAsync(int userId)
    {
        var friends = await _unitOfWork.FriendshipRepository.GetSenderFriendshipsAsync(userId);
        return _mapper.Map<UserFriendshipsModel>(friends);
    }

    public async Task UpdateAsync(int id, UserUpdateModel userModel)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user is null) throw new NotFoundException($"User by id {id} not found");
        user.Id = id;
        _mapper.Map(userModel, user);
        _unitOfWork.UserRepository.Update(user);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateFriendshipAsync(int userId, int friendId, UserFriendshipUpdateModel friendshipModel)
    {
        var friendship = await _unitOfWork.FriendshipRepository.GetByIdAsync(userId, friendId);
        if (friendship is null) throw new NotFoundException("Friendship not found");
        friendship.SenderId = userId;
        friendship.ReceiverId = friendId;
        _mapper.Map(friendshipModel, friendship);
        _unitOfWork.FriendshipRepository.Update(friendship);
        await _unitOfWork.SaveAsync();
    }
}