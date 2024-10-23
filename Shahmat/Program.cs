class Shahmat
{
    private static int boardSize = 8;

    static int[] knightMovesRow = { -2, -1, 1, 2, 2, 1, -1, -2 };
    static int[] knightMovesCol = { 1, 2, 2, 1, -1, -2, -2, -1 };

    static Random random = new();

    static void Main(string[] args)
    {
        (int knightRow, int knightCol) = GetPosition();
        (int rookRow, int rookCol) = GetPosition();

        while (knightRow == rookRow && knightCol == rookCol)
        {
            (rookRow, rookCol) = GetPosition();
        }

        Console.WriteLine($"Atin bashlangic movgeyi: ({knightRow}, {knightCol})");
        Console.WriteLine($"Topun bashlangic movgeyi: ({rookRow}, {rookCol})");

        while (knightRow != rookRow || knightCol != rookCol)
        {
            List<(int, int)> validMoves = GetValidKnightMoves(knightRow, knightCol);

            Console.WriteLine($"\nAtin movgeyi: ({knightRow}, {knightCol})");
            Console.WriteLine("Mumkun gedishler:");

            foreach (var move in validMoves)
            {
                Console.WriteLine($"Gedish: ({move.Item1}, {move.Item2})");
            }

            (int nextRow, int nextCol) = GetBestMove(knightRow, knightCol, rookRow, rookCol, validMoves);
            Console.WriteLine($"Sechdi: ({nextRow}, {nextCol})");

            knightRow = nextRow;
            knightCol = nextCol;
        }

        Console.WriteLine($"\nAt topu vurdu! Movge: ({knightRow}, {knightCol})");
    }

    static (int, int) GetPosition()
    {
        return (random.Next(boardSize), random.Next(boardSize));
    }

    static List<(int, int)> GetValidKnightMoves(int row, int col)
    {
        List<(int, int)> moves = [];

        for (int i = 0; i < knightMovesRow.Length; i++)
        {
            int newRow = row + knightMovesRow[i];
            int newCol = col + knightMovesCol[i];

            if (IsValidPosition(newRow, newCol))
            {
                moves.Add((newRow, newCol));
            }
        }

        return moves;
    }

    static (int, int) GetBestMove(int knightRow, int knightCol, int rookRow, int rookCol, List<(int, int)> validMoves)
    {
        (int, int) bestMove = validMoves[0];
        int minDistance = GetDistance(validMoves[0].Item1, validMoves[0].Item2, rookRow, rookCol);

        foreach (var move in validMoves)
        {
            int distance = GetDistance(move.Item1, move.Item2, rookRow, rookCol);
            if (distance < minDistance)
            {
                minDistance = distance;
                bestMove = move;
            }
        }

        return bestMove;
    }

    static bool IsValidPosition(int row, int col)
    {
        return row >= 0 && row < boardSize && col >= 0 && col < boardSize;
    }

    //Manhattan usulu
    static int GetDistance(int row1, int col1, int row2, int col2)
    {
        return Math.Abs(row1 - row2) + Math.Abs(col1 - col2);
    }
}