using Data;
using Data.Enums;
using Data.Models.Game;
using Data.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Dto.Game;

namespace TicTacToe.Services;

public class GameService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    public GameService(ApplicationDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<LobbyDto> CreateGameAsync(User creator, BoardValue side)
    {
        var lobby = new Lobby(creator, side);
        
        await _dbContext.Lobbies.AddAsync(lobby);
        await _dbContext.SaveChangesAsync();

        return new LobbyDto(lobby, side);
    }

    public async Task<(LobbyDto?, string?)> JoinGameAsync(long lobbyId, User user)
    {
        var lobby = await _dbContext.Lobbies
            .Include(l => l.XUser)
            .Include(l => l.OUser)
            .Include(l => l.Board)
            .FirstOrDefaultAsync(l => l.Id == lobbyId);
        if (lobby == null)
            return (null, "No lobby with this id found.");

        BoardValue side;
        if (lobby.XUser == null)
        {
            lobby.XUser = user;
            side = BoardValue.X;
        }
        else if (lobby.OUser == null)
        {
            lobby.OUser = user;
            side = BoardValue.O;
        }
        else
            return (null, "Lobby is already full.");

        lobby.IsStared = true;

        await _dbContext.SaveChangesAsync();

        return (new LobbyDto(lobby, side), null);
    }

    public async Task<(LobbyDto?, string?)> MakeTurnAsync(long lobbyId, User user, int x, int y)
    {
        var lobby = await _dbContext.Lobbies
            .Include(l => l.XUser)
            .Include(l => l.OUser)
            .Include(l => l.Board)
            .FirstOrDefaultAsync(l => l.Id == lobbyId);
        if (lobby == null)
            return (null, "No lobby with this id found.");
        
        if (lobby.IsStared == false)
            return (null, "The game hasn't started yet.");
        
        BoardValue side;
        if (lobby.XUser?.Id == user.Id)
            side = BoardValue.X;
        else if (lobby.OUser?.Id == user.Id)
            side = BoardValue.O;
        else return (null, "You are not in this lobby.");
        
        if (lobby.Turn != side)
            return (null, "It's not your turn yet.");

        if (lobby.Board[x, y] != null)
            return (null, "Field is already occupied.");
        
        lobby.Board[x, y] = side;
        lobby.Board.TurnCount++;
        lobby.Turn = lobby.Turn == BoardValue.X ? BoardValue.O : BoardValue.X;

        
        var draw = lobby.Board.CheckDraw();
        if (draw)
        {
            var xUser = await _userManager.FindByIdAsync(lobby.XUser!.Id);
            var oUser = await _userManager.FindByIdAsync(lobby.OUser!.Id);
            
            xUser.Games++;
            oUser.Games++;

            await _userManager.UpdateAsync(xUser);
            await _userManager.UpdateAsync(oUser);

            _dbContext.Lobbies.Remove(lobby);
            await _dbContext.SaveChangesAsync();

            return (new LobbyDto(lobby, side, GameResult.Draw), null);
        }
        
        var victory = lobby.Board.CheckVictory(x, y, side);
        if (victory)
        {
            var xUser = await _userManager.FindByIdAsync(lobby.XUser!.Id);
            var oUser = await _userManager.FindByIdAsync(lobby.OUser!.Id);
            GameResult? result = null;
            switch (side)
            {
                case BoardValue.X:
                    xUser.Games++;
                    xUser.Wins++;
                    oUser.Games++;
                    result = GameResult.XWin;
                    break;
                case BoardValue.O:
                    xUser.Games++;
                    oUser.Games++;
                    oUser.Wins++;
                    result = GameResult.OWin;
                    break;
            }

            await _userManager.UpdateAsync(xUser);
            await _userManager.UpdateAsync(oUser);

            _dbContext.Lobbies.Remove(lobby);
            await _dbContext.SaveChangesAsync();

            return (new LobbyDto(lobby, side, result), null);
        }
        
        await _dbContext.SaveChangesAsync();
        
        return (new LobbyDto(lobby, side), null);
    }
}