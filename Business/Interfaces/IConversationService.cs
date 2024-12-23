using Business.Models.Conversation;

namespace Business.Interfaces;

public interface IConversationService
{
    Task<ConversationsModel> GetByUserIdAsync(int userId);
    
    Task<ConversationModel> GetByIdAsync(int userId, int conversationId);
    
    Task CreateAsync(int userId, ConversationCreateModel conversationModel);
    
    Task UpdateAsync(int conversationId, int currentUserId, ConversationUpdateModel conversationModel);
    
    Task UpdateParticipantAsync(int conversationId, int currentUserId, int userId, ConversationParticipantUpdateModel participantModel);
}