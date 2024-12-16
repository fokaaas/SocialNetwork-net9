using Data.Entities;

namespace Data.Interfaces;

public interface IFriendshipRepository : IRepository<Friendship>
{
    Task<Friendship> GetByIdAsync(int senderId, int receiverId);
    
    Task DeleteByIdAsync(int senderId, int receiverId);
}