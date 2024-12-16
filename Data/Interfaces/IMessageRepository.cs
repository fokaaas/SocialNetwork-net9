using Data.Entities;

namespace Data.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    Task<Message> GetByIdAsync(int id);
    
    Task DeleteByIdAsync(int id);
}