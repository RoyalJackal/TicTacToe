using Data.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Dto.Auth;
using TicTacToe.Services;

namespace TicTacToe.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;

    public AuthController(UserManager<User> userManager, TokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost]  
    [Route("login")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<TokenDto>> SignIn([FromBody] SignInDto dto)
    {

        var user = await _userManager.FindByNameAsync(dto.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password)) return Unauthorized();
        
        var (accessToken, expiration) = _tokenService.CreateToken(user);
        return Ok(new TokenDto(accessToken, expiration));
    }
    
    [HttpPost]  
    [Route("register")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TokenDto>> SignUp([FromBody] SignUpDto dto)
    {
        var userExists = await _userManager.FindByNameAsync(dto.Username);
        if (userExists != null)
            return BadRequest("Username occupied.");
        
        var user = new User {UserName = dto.Username, Email = dto.Email};
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded) return BadRequest("Undocumented error.");
        
        var (accessToken, expiration) = _tokenService.CreateToken(user);
        return Ok(new TokenDto(accessToken, expiration));
    }
}