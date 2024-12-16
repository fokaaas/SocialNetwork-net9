using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ConversationParticipantRepository : IConversationParticipantRepository
{
    private readonly SocialNetworkDbContext _context;
    
    public ConversationParticipantRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }
    
    public async Task<ConversationParticipant> GetByIdAsync(int conversationId, int participantId)
    {
        return await _context.ConversationParticipants.FindAsync(conversationId, participantId);
    }
    
    public async Task DeleteByIdAsync(int conversationId, int participantId)
    {
        var conversationParticipant = await _context.ConversationParticipants.FindAsync(conversationId, participantId);
        if (conversationParticipant is not null)
        {
            _context.ConversationParticipants.Remove(conversationParticipant);
        }
    }
    
    public async Task AddAsync(ConversationParticipant entity)
    {
        await _context.ConversationParticipants.AddAsync(entity);
    }
    
    public void Delete(ConversationParticipant entity)
    {
        _context.ConversationParticipants.Remove(entity);
    }
    
    public async Task<IEnumerable<ConversationParticipant>> GetAllAsync()
    {
        return await _context.ConversationParticipants.ToListAsync();
    }
    
    public void Update(ConversationParticipant entity)
    {
        _context.ConversationParticipants.Update(entity);
    }
}