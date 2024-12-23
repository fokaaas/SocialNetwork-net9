using Business.Interfaces;
using Business.Models.Message;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    
    public MessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<MessageModel>> GetById(int id)
    {
        var result = await _messageService.GetByIdAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] MessageCreateModel messageModel)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        await _messageService.CreateAsync(Convert.ToInt32(userId), messageModel);
        return StatusCode(StatusCodes.Status201Created);
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] MessageUpdateModel messageModel)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        await _messageService.UpdateAsync(Convert.ToInt32(userId), id, messageModel);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userId = HttpContext.Items["UserId"].ToString();
        await _messageService.DeleteAsync(Convert.ToInt32(userId), id);
        return Ok();
    }
}