using System.ComponentModel.DataAnnotations;

namespace Business.Models.Conversation;

public class ConversationCreateGroupDetailsModel
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
    public string Name { get; set; }

    [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
    public string? Description { get; set; }

    [Url(ErrorMessage = "Invalid URL format for avatar link")]
    public string? AvatarLink { get; set; }
}