using Data.Enums;
using Data.Models.Game;

namespace TicTacToe.Dto.Game;

public class LobbyDto
{
    public LobbyDto(Lobby lobby, BoardValue side, GameResult? result = null)
    {
        Id = lobby.Id;
        Row1 = lobby.Board.Row1;
        Row2 = lobby.Board.Row2;
        Row3 = lobby.Board.Row3;
        XUserId = lobby.XUser?.Id;
        XUserName = lobby.XUser?.UserName;
        OUserId = lobby.OUser?.Id;
        OUserName = lobby.OUser?.UserName;
        Turn = lobby.Turn;
        Side = side;
        Result = result;
    }
    
    public long Id { get; set; }
    
    public List<BoardValue?> Row1 { get; set; }
    
    public List<BoardValue?> Row2 { get; set; }
    
    public List<BoardValue?> Row3 { get; set; }
    
    public string? XUserId { get; set; }
    
    public string? XUserName { get; set; }
    
    public string? OUserId { get; set; }
    
    public string? OUserName { get; set; }
    
    public BoardValue Turn { get; set; }
    
    public BoardValue Side { get; set; }
    
    public GameResult? Result { get; set; }
}