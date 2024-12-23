using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ConversationRepository : IConversationRepository
{
    private readonly SocialNetworkDbContext _context;

    public ConversationRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var conversation = await _context.Conversations.FindAsync(id);
        if (conversation is not null) _context.Conversations.Remove(conversation);
    }

    public async Task<IEnumerable<Conversation>> GetManyByUserIdAsync(int userId)
    {
        return await _context.Conversations
            .Include(c => c.GroupDetails)
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .Where(c => c.Participants.Any(p => p.UserId == userId))
            .ToListAsync();
    }

    public async Task<Conversation> GetByIdAsync(int id)
    {
        var conversations = await _context.Conversations
            .Include(c => c.GroupDetails)
            .Include(c => c.Participants)
            .ThenInclude(p => p.User)
            .Include(c => c.Messages)
            .ThenInclude(m => m.Sender)
            .FirstOrDefaultAsync(c => c.Id == id);

        conversations.Messages = conversations.Messages
            .OrderByDescending(m => m.CreatedAt).ToList();

        return conversations;
    }

    public async Task AddAsync(Conversation entity)
    {
        await _context.Conversations.AddAsync(entity);
    }

    public void Delete(Conversation entity)
    {
        _context.Conversations.Remove(entity);
    }

    public void Update(Conversation entity)
    {
        _context.Conversations.Update(entity);
    }

    public async Task<IEnumerable<Conversation>> GetAllAsync()
    {
        return await _context.Conversations.ToListAsync();
    }
}