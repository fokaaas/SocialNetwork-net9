using Data.Entities;

namespace Business.Models.User;

public class UserFriendshipModel
{
    public int SenderId { get; set; }
    
    public int ReceiverId { get; set; }

    public string SenderName { get; set; }
    
    public string ReceiverName { get; set; }

    public string SenderSurname { get; set; }
    
    public string ReceiverSurname { get; set; }

    public string? SenderAvatarLink { get; set; }
    
    public string? ReceiverAvatarLink { get; set; }

    public FriendshipStatus Status { get; set; }
}