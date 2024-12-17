namespace Business.Models.User;

public class ShortUserModel
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string Surname { get; set; }

    public string? AvatarLink { get; set; }
}