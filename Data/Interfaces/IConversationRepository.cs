using Data.Entities;

namespace Data.Interfaces;

public interface IConversationRepository: IRepository<Conversation>
{
    Task<IEnumerable<Conversation>> GetAllWithDetailsAsync();
    
    Task<Conversation> GetByIdWithDetailsAndMessagesAsync(int id);
}