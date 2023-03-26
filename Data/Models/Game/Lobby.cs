using Data.Enums;
using Data.Models.Users;

namespace Data.Models.Game;

public class Lobby
{
    public Lobby(User creator, BoardValue side)
    {
        Board = new Board();
        switch (side)
        {
            case BoardValue.X: 
                XUser = creator;
                break;
            case BoardValue.O:
                OUser = creator;
                break;
        }
    }

    public Lobby()
    {
    }
    public long Id { get; set; }
    
    public Board Board { get; set; }

    public User? XUser { get; set; }

    public User? OUser { get; set; }

    public BoardValue Turn { get; set; } = BoardValue.X;

    public bool IsStared { get; set; } = false;

    public BoardValue? GetUserSide(User user)
    {
        if (XUser != null && user.Id == XUser.Id)
            return BoardValue.X;
        if (OUser != null && user.Id == OUser.Id)
            return BoardValue.X;
        return null;
    }
}