namespace Data.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string? AvatarLink { get; set; }

    public DateTime CreatedAt { get; set; }


    public ICollection<Friendship> FriendshipsAsSender { get; init; } = new List<Friendship>();
    
    public ICollection<Friendship> FriendshipsAsReceiver { get; init; } = new List<Friendship>();

    public ICollection<Message> Messages { get; init; } = new List<Message>();

    public ICollection<ConversationParticipant> ConversationParticipants { get; init; } =
        new List<ConversationParticipant>();
}