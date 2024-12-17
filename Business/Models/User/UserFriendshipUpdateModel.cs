using Data.Entities;

namespace Business.Models.User;

public class UserFriendshipUpdateModel
{
    public FriendshipStatus Status { get; set; }
}