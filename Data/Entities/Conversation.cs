namespace Data.Entities;

public class Conversation : BaseEntity
{
    public bool IsGroup { get; set; }

    public int? GroupDetailsId { get; set; }

    public GroupDetails? GroupDetails { get; set; }

    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();
}