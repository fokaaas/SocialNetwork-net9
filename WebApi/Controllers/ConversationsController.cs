using Business.Interfaces;
using Business.Models.Conversation;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConversationsController : ControllerBase
{
    private readonly IConversationService _conversationService;
    
    public ConversationsController(IConversationService conversationService)
    {
        _conversationService = conversationService;
    }
    
    [HttpGet]
    public async Task<ActionResult<ConversationsModel>> GetManyByUserId()
    {
        var userId = HttpContext.Items["UserId"].ToString();
        var result = await _conversationService.GetByUserIdAsync(Convert.ToInt32(userId));
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ConversationModel>> GetById(int id)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        var result = await _conversationService.GetByIdAsync(Convert.ToInt32(userId), id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ConversationCreateModel conversationModel)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        await _conversationService.CreateAsync(Convert.ToInt32(userId), conversationModel);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] ConversationUpdateModel conversationModel)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        await _conversationService.UpdateAsync(id, Convert.ToInt32(userId), conversationModel);
        return Ok();
    }
    
    [HttpPatch("{conversationId}/users/{userId}")]
    public async Task<ActionResult> UpdateParticipant(int conversationId, int userId, [FromBody] ConversationParticipantUpdateModel participantModel)
    {
        var currentUserId = HttpContext.Items["UserId"].ToString();
        await _conversationService.UpdateParticipantAsync(conversationId, Convert.ToInt32(currentUserId), userId, participantModel);
        return Ok();
    }
}