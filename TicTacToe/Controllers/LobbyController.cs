using Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Dto.Game;
using TicTacToe.Services;

namespace TicTacToe.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController : AuthorizedControllerBase
{
    private readonly LobbyService _lobbyService;

    public LobbyController(UserManager<User> userManager, LobbyService lobbyService) : base(userManager)
    {
        _lobbyService = lobbyService;
    }

    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(ListLobbyDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ListLobbyDto>>> GetAll() =>
        Ok(await _lobbyService.GetAllLobbiesAsync());
    
    [HttpGet]
    [Route("open")]
    [ProducesResponseType(typeof(ListLobbyDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ListLobbyDto>>> GetOpen() =>
        Ok(await _lobbyService.GetOpenLobbiesAsync());

    [HttpGet]
    [Authorize]
    [Route("user")]
    [ProducesResponseType(typeof(ListLobbyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ListLobbyDto>>> GetUser()
    {
        var (user, error) = await CheckAuth();
        if (user == null)
            return BadRequest(error);
        
        return Ok(await _lobbyService.GetUserLobbiesAsync(user));
    }
        
}