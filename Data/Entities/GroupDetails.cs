namespace Data.Entities;

public class GroupDetails : BaseEntity
{
    public int ConversationId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? AvatarLink { get; set; }

    public Conversation Conversation { get; set; }
}