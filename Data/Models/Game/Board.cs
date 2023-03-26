using Data.Enums;

namespace Data.Models.Game;

public class Board
{
    public Board()
    {
        Row1 = new List<BoardValue?> {null, null, null};
        Row2 = new List<BoardValue?> {null, null, null};
        Row3 = new List<BoardValue?> {null, null, null};
    }

    public long Id { get; set; }
    
    public List<BoardValue?> Row1 { get; set; }
    public List<BoardValue?> Row2 { get; set; }
    public List<BoardValue?> Row3 { get; set; }

    public int TurnCount { get; set; } = 0;

    public BoardValue? this[int i1, int i2]
    {
        get
        {
            switch (i1)
            {
                case 0: return Row1[i2];
                case 1: return Row2[i2];
                case 2: return Row3[i2];
                default: throw new IndexOutOfRangeException();
            }
        }
        set
        {
            switch (i1)
            {
                case 0: 
                    Row1[i2] = value;
                    break;
                case 1: 
                    Row2[i2] = value;
                    break;
                case 2: 
                    Row3[i2] = value;
                    break;
                default: throw new IndexOutOfRangeException();
            }
        }
    }

    public bool CheckVictory(int x, int y, BoardValue side)
    {
        //check col
        for(int i = 0; i < 3; i++){
            if(this[x,i] != side)
                break;
            if(i == 2)
                return true;
        }
        
        //check row
        for(int i = 0; i < 3; i++){
            if(this[i,y] != side)
                break;
            if (i == 2)
                return true;
        }
        
        //check diag
        if(x == y){
            for(int i = 0; i < 3; i++){
                if(this[i,i] != side)
                    break;
                if (i == 2)
                    return true;
            }
        }
            
        //check anti diag
        if(x + y == 2){
            for(int i = 0; i < 3; i++){
                if(this[i,2-i] != side)
                    break;
                if(i == 2)
                    return true;
            }
        }

        return false;
    }

    public bool CheckDraw() => TurnCount >= 9;
}