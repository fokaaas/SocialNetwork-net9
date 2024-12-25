using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly SocialNetworkDbContext _context;

    public FriendshipRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }

    public async Task<Friendship> GetByIdAsync(int senderId, int receiverId)
    {
        return await _context.Friendships.FindAsync(senderId, receiverId);
    }

    public async Task DeleteByIdAsync(int senderId, int receiverId)
    {
        var friendship = await _context.Friendships.FindAsync(senderId, receiverId);
        if (friendship is not null) _context.Friendships.Remove(friendship);
    }

    public async Task AddAsync(Friendship entity)
    {
        await _context.Friendships.AddAsync(entity);
    }

    public void Delete(Friendship entity)
    {
        _context.Friendships.Remove(entity);
    }

    public void Update(Friendship entity)
    {
        _context.Friendships.Update(entity);
    }

    public async Task<IEnumerable<Friendship>> GetFriendshipsAsync(int id)
    {
        return await _context.Friendships
            .Where(f => f.SenderId == id || f.ReceiverId == id)
            .Include(f => f.Receiver)
            .Include(f => f.Sender)
            .ToListAsync();
    }

    public async Task<IEnumerable<Friendship>> GetAllAsync()
    {
        return await _context.Friendships.ToListAsync();
    }
}