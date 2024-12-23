using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly SocialNetworkDbContext _context;

    public MessageRepository(SocialNetworkDbContext context)
    {
        _context = context;
    }

    public async Task<Message> GetByIdAsync(int id)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message is not null) _context.Messages.Remove(message);
    }

    public async Task AddAsync(Message entity)
    {
        await _context.Messages.AddAsync(entity);
    }

    public void Delete(Message entity)
    {
        _context.Messages.Remove(entity);
    }

    public void Update(Message entity)
    {
        _context.Messages.Update(entity);
    }

    public async Task<IEnumerable<Message>> GetAllAsync()
    {
        return await _context.Messages.ToListAsync();
    }
}