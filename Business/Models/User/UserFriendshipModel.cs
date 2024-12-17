using Data.Entities;

namespace Business.Models.User;

public class UserFriendshipModel : ShortUserModel
{
    public FriendshipStatus Status { get; set; }
}