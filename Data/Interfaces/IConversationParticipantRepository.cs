using Data.Entities;

namespace Data.Interfaces;

public interface IConversationParticipantRepository : IRepository<ConversationParticipant>
{
    Task<ConversationParticipant> GetByIdAsync(int conversationId, int participantId);

    Task DeleteByIdAsync(int conversationId, int participantId);

    Task<IEnumerable<ConversationParticipant>> GetManyByConversationId(int conversationId);
}