namespace Business.Models.User;

public class UserModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string? AvatarLink { get; set; }

    public DateTime JoinedAt { get; set; }
}