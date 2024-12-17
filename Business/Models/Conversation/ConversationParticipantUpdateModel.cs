using Data.Entities;

namespace Business.Models.Conversation;

public class ConversationParticipantUpdateModel
{
    public ConversationRole? Role { get; set; }
}