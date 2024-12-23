using Data.Entities;

namespace Business.Models.Conversation;

public class ConversationUserModel
{
    public int UserId { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }
    
    public ConversationRole Role { get; set; }
    
    public string? AvatarLink { get; set; }
    
    public DateTime JoinedAt { get; set; }
}