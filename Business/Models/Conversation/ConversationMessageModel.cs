namespace Business.Models.Conversation;

public class ConversationMessageModel
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
}