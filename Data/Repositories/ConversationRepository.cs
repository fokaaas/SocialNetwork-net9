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
    
    public async Task<Conversation> GetByIdAsync(int id)
    {
        return await _context.Conversations.FindAsync(id);
    }
    
    public async Task DeleteByIdAsync(int id)
    {
        var conversation = await _context.Conversations.FindAsync(id);
        if (conversation is not null)
        {
            _context.Conversations.Remove(conversation);
        }
    }
    
    public async Task<IEnumerable<Conversation>> GetAllWithGroupDetailsAsync()
    {
        return await _context.Conversations
            .Include(c => c.GroupDetails)
            .ToListAsync();
    }
    
    public async Task<Conversation> GetByIdWithGroupDetailsAndMessagesAsync(int id)
    {
        return await _context.Conversations
            .Include(c => c.GroupDetails)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task AddAsync(Conversation entity)
    {
        await _context.Conversations.AddAsync(entity);
    }
    
    public void Delete(Conversation entity)
    {
        _context.Conversations.Remove(entity);
    }
    
    public async Task<IEnumerable<Conversation>> GetAllAsync()
    {
        return await _context.Conversations.ToListAsync();
    }
    
    public void Update(Conversation entity)
    {
        _context.Conversations.Update(entity);
    }
}