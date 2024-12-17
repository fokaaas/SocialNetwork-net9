using Business.Models.Conversation;

namespace Business.Models.Message;

public class MessageModel : ConversationMessageModel
{
    public int ConversationId { get; set; }
}