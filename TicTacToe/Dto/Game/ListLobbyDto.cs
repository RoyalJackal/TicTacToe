using Data.Models.Game;

namespace TicTacToe.Dto.Game;

public class ListLobbyDto
{
    public ListLobbyDto(Lobby lobby)
    {
        Id = lobby.Id;
        XUserId = lobby.XUser?.Id;
        XUserName = lobby.XUser?.UserName;
        OUserId = lobby.OUser?.Id;
        OUserName = lobby.OUser?.UserName;
    }
    
    public long Id { get; set; }

    public string? XUserId { get; set; }
    
    public string? XUserName { get; set; }
    
    public string? OUserId { get; set; }
    
    public string? OUserName { get; set; }
}