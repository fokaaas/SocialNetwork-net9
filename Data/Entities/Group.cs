namespace Data.Entities;

public class Group : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    
    public ICollection<GroupMember> Members { get; init; } = new List<GroupMember>();
}