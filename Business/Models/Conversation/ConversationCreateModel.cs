using System.ComponentModel.DataAnnotations;

namespace Business.Models.Conversation;

public class ConversationCreateModel
{
    [Required(ErrorMessage = "Participant ids is required")]
    public ICollection<int> ParticipantIds { get; set; } = new List<int>();

    public ConversationCreateGroupDetailsModel? GroupDetails { get; set; }
}