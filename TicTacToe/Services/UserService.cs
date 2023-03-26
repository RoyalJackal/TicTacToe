using Data.Models.Users;
using Microsoft.AspNetCore.Identity;
using TicTacToe.Dto.User;

namespace TicTacToe.Services;

public class UserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<(UserInfoDto?, string?)> GetUserInfoAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return (null, "No user with this ID.");

        return (new UserInfoDto
        {
            Username = user.UserName,
            Games = user.Games,
            Wins = user.Wins
        }, null);
    }
}