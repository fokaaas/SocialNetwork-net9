using Data.Entities;

namespace Data.Interfaces;

public interface IConversationRepository: IRepository<Conversation>
{
    Task<Conversation> GetByIdAsync(int id);
    
    Task DeleteByIdAsync(int id);
    
    Task<IEnumerable<Conversation>> GetAllWithGroupDetailsAsync();
    
    Task<Conversation> GetByIdWithGroupDetailsAndMessagesAsync(int id);
}