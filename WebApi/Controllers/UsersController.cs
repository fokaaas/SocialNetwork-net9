using Business.Interfaces;
using Business.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<UsersModel>> GetMany()
    {
        var result = await _userService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserModel>> GetById(int id)
    {
        var result = await _userService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UserUpdateModel userModel)
    {
        await _userService.UpdateAsync(id, userModel);
        return Ok();
    }

    [HttpPost("{receiverId}/friendships")]
    public async Task<ActionResult> CreateFriendship(int receiverId)
    {
        var currentUserId = HttpContext.Items["UserId"].ToString();
        await _userService.CreateFriendshipAsync(Convert.ToInt32(currentUserId), receiverId);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpGet("{senderId}/friendships")]
    public async Task<ActionResult<UserFriendshipsModel>> GetFriendships(int senderId)
    {
        var result = await _userService.GetFriendsAsync(senderId);
        return Ok(result);
    }

    [HttpPatch("{senderId}/friendships/{receiverId}")]
    public async Task<ActionResult> UpdateFriendship(int senderId, int receiverId,
        [FromBody] UserFriendshipUpdateModel friendshipModel)
    {
        await _userService.UpdateFriendshipAsync(senderId, receiverId, friendshipModel);
        return Ok();
    }

    [HttpDelete("{senderId}/friendships/{receiverId}")]
    public async Task<ActionResult> DeleteFriendship(int senderId, int receiverId)
    {
        await _userService.DeleteFriendshipAsync(senderId, receiverId);
        return Ok();
    }
}