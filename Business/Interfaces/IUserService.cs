using Business.Models.User;

namespace Business.Interfaces;

public interface IUserService
{
    Task<UserModel> GetByIdAsync(int userId);
    
    Task<UsersModel> GetAllAsync();
    
    Task CreateFriendshipAsync(int userId, int friendId);
    
    Task<UserFriendshipsModel> GetFriendsAsync(int userId);
    
    Task UpdateFriendshipAsync(int userId, int friendId, UserFriendshipUpdateModel friendshipModel);
    
    Task DeleteFriendshipAsync(int userId, int friendId);
}