using Data.Entities;

namespace Data.Interfaces;

public interface IConversationRepository: IRepository<Conversation>
{
    Task DeleteByIdAsync(int id);
    
    Task<IEnumerable<Conversation>> GetManyByUserIdAsync(int userId);
    
    Task<Conversation> GetByIdAsync(int id);
}