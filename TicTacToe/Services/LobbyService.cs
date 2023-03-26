using Data;
using Data.Models.Users;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Dto.Game;

namespace TicTacToe.Services;

public class LobbyService
{
    private readonly ApplicationDbContext _dbContext;

    public LobbyService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ListLobbyDto>> GetAllLobbiesAsync() =>
        await _dbContext.Lobbies
            .Include(l => l.XUser)
            .Include(l => l.OUser)
            .Select(l => new ListLobbyDto(l))
            .ToListAsync();
    
    public async Task<List<ListLobbyDto>> GetOpenLobbiesAsync() =>
        await _dbContext.Lobbies
            .Include(l => l.XUser)
            .Include(l => l.OUser)
            .Where(l => !l.IsStared)
            .Select(l => new ListLobbyDto(l))
            .ToListAsync();
    
    public async Task<List<ListLobbyDto>> GetUserLobbiesAsync(User user) =>
        await _dbContext.Lobbies
            .Include(l => l.XUser)
            .Include(l => l.OUser)
            .Where(l => (l.XUser != null && l.XUser.Id == user.Id) || (l.OUser != null && l.OUser.Id == user.Id))
            .Select(l => new ListLobbyDto(l))
            .ToListAsync();
}