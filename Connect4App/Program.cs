using System;
using System.Drawing;


public class BoardClass
{
    const int BoardWidth = 6;
    string[] Columns;
    public BoardClass()
    {
        Columns = new string[7] { "", "", "", "", "", "", "" };
    }

    public void PlayMove(int whichColumn, char whichPlayer)
    {
        Columns[whichColumn] += char.ToUpper(whichPlayer);
        DrawBoard();
        //Did someone win
    }

    public void DrawGrid()
    {
        string Row = "| | | | | | | |";
        string RowSeparator = "|-|-|-|-|-|-|-|";
        Console.Clear();
        Console.WriteLine("BoardClass !");
        for (int jRow = 0; jRow < BoardWidth; jRow++)
        {
            Console.WriteLine(Row);
            Console.WriteLine(RowSeparator);
        }
    }
    public void DrawPieces()
    {
        for (int iCol = 0; iCol < Columns.Length; iCol++)
        {
            var column = Columns[iCol];
            for (int jRow = 0; jRow < column.Length; jRow++)
            {
                var piece = column[jRow];
                ConsoleColor color = piece switch
                {
                    'R' => ConsoleColor.Red,
                    'Y' => ConsoleColor.Yellow,
                    _ => throw new Exception($"unexpected piece {piece} ")
                };
                Console.SetCursorPosition(iCol * 2 + 1, (5 - jRow) * 2 + 1);
                Console.BackgroundColor = color;
                Console.Write(" ");
            }
            Console.ResetColor();
        }
    }
    public void DrawBoard()
    {
        DrawGrid();
        DrawPieces();
    }
}

class Program
{
    public static (int moveColumn,char player) GetMove(char turn)
    {
        while (true)
        {
            Console.SetCursorPosition(0, 20);
            Console.Write("Enter Move");
            var k = Console.ReadKey();
            char cMove = k.KeyChar;
            bool moveIsOk = int.TryParse("" + cMove,out int nMove);
            if (!moveIsOk) continue;
            moveIsOk = (nMove >= 0) && (nMove<7);
            if (!moveIsOk) continue;
            return (moveColumn:nMove,player:turn);
        }
    }
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        var board = new BoardClass();
        board.DrawGrid();
        while (true)
        {
            var moveR = GetMove('R');
            board.PlayMove(moveR.moveColumn, moveR.player);
            var moveY = GetMove('Y');
            board.PlayMove(moveY.moveColumn, moveY.player);
        }
    }
}