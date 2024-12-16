namespace Data.Entities;

public class Message : BaseEntity
{
    public int SenderId { get; set; }

    public int ConversationId { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public User Sender { get; set; }

    public Conversation Conversation { get; set; }
}