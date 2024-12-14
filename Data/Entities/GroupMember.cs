namespace Data.Entities;

public class GroupMember
{
    public int UserId { get; set; }
    
    public int GroupId { get; set; }
    
    public GroupRole Role { get; set; }
    
    public DateTime JoinedAt { get; set; }
    
    
    public User User { get; set; }
    
    public Group Group { get; set; }
    
}