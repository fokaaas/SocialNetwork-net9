using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class ConversationParticipant
{
    public int UserId { get; set; }
    
    public int ConversationId { get; set; }

    public ConversationRole Role { get; set; }
    
    public User User { get; set; }
    
    public Conversation Conversation { get; set; }
}