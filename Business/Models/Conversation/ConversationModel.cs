using Data.Entities;

namespace Business.Models.Conversation;

public class ConversationModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? AvatarLink { get; set; }
    
    public bool IsGroup { get; set; }
    
    public ICollection<ConversationMessageModel> Messages { get; set; } = new List<ConversationMessageModel>();
    
    public ICollection<ConversationUserModel> Participants { get; set; } = new List<ConversationUserModel>();
}