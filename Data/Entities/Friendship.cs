namespace Data.Entities;

public class Friendship
{
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }

    public FriendshipStatus Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    
    public User Sender { get; set; }
    
    public User Receiver { get; set; }
}