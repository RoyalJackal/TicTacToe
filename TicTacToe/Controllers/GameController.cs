using Data.Enums;
using Data.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Dto.Game;
using TicTacToe.Services;

namespace TicTacToe.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : AuthorizedControllerBase
{
    private readonly GameService _gameService;

    public GameController(UserManager<User> userManager, GameService gameService) : base(userManager)
    {
        _gameService = gameService;
    }
    
    [HttpPost]
    [Authorize]
    [Route("create")]
    [ProducesResponseType(typeof(LobbyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ListLobbyDto>>> CreateGame([FromBody] BoardValue side)
    {
        var (user, error) = await CheckAuth();
        if (user == null)
            return BadRequest(error);

        return Ok(await _gameService.CreateGameAsync(user, side));
    }
    
    [HttpPost]
    [Authorize]
    [Route("join/{id:long}")]
    [ProducesResponseType(typeof(LobbyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ListLobbyDto>>> JoinGame(long id)
    {
        var (user, userError) = await CheckAuth();
        if (user == null)
            return BadRequest(userError);

        var (lobby, lobbyError) = await _gameService.JoinGameAsync(id, user);
        if (lobby == null)
            return BadRequest(lobbyError);
        
        return Ok(lobby);
    }
    
    [HttpPost]
    [Authorize]
    [Route("turn/{id:long}")]
    [ProducesResponseType(typeof(LobbyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<ListLobbyDto>>> MakeTurn([FromBody] MakeTurnDto dto, long id)
    {
        var (user, userError) = await CheckAuth();
        if (user == null)
            return BadRequest(userError);

        var (lobby, lobbyError) = await _gameService.MakeTurnAsync(id, user, dto.X, dto.Y);
        if (lobby == null)
            return BadRequest(lobbyError);
        
        return Ok(lobby);
    }
}