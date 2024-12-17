namespace Data.Entities;

public class Conversation : BaseEntity
{
    public bool IsGroup { get; set; }

    public int? GroupDetailsId { get; set; }

    public GroupDetails? GroupDetails { get; set; }

    public ICollection<Message> Messages { get; init; } = new List<Message>();

    public ICollection<ConversationParticipant> Participants { get; init; } = new List<ConversationParticipant>();
}