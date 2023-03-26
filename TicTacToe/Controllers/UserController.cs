using Microsoft.AspNetCore.Mvc;
using TicTacToe.Dto.User;
using TicTacToe.Services;

namespace TicTacToe.Controllers;

public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    [Route("info/{id}")]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<UserInfoDto>>> JoinGame(string id)
    {
        var (userInfo, error) = await _userService.GetUserInfoAsync(id);
        if (userInfo == null)
            return BadRequest(error);

        return Ok(userInfo);
    }
}