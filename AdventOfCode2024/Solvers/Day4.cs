namespace AdventOfCode2024.Solvers;

public class Day4 : Solver
{
    private char[][] _grid;

    public Day4(string[] input) : base(input)
    {
        _grid = null!;
    }

    public static Day4 FromFile() => new Day4(File.ReadAllLines("inputs/Day4.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        base.OnPrepare(inputs);
        _grid = new char[inputs.Length][];
        for (var i = 0; i < inputs.Length; i++)
        {
            var line = inputs[i];
            _grid[i] = line.ToCharArray();
        }
    }

    protected override int Part1()
    {
        char[] targetWord = { 'X', 'M', 'A', 'S' };
        int count = 0;

        var checks = new[]
        {
            LeftRightHorizontal,
            RightLeftHorizontal,
            UpDownVertical,
            DownUpVertical,
            TopLeftBottomRightDiagonal,
            BottomRightTopLeftDiagonal,
            TopRightBottomLeftDiagonal,
            BottomLeftTopRightDiagonal
        };

        for (var i = 0; i < _grid.Length; i++)
        {
            var line = _grid[i];
            for (var j = 0; j < line.Length; j++)
            {
                count += checks.Count(check => check(targetWord, _grid, i, j));
            }
        }

        return count;
    }

    protected override int Part2()
    {
        int count = 0;
        for (var i = 0; i < _grid.Length; i++)
        {
            var line = _grid[i];
            for (var j = 0; j < line.Length; j++)
            {
                var currentChar = _grid[i][j];
                if (currentChar == 'A' && IsXMas(i, j, _grid))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static bool IsXMas(int x, int y, char[][] grid)
    {
        if (x - 1 < 0 || y - 1 < 0)
        {
            return false;
        }

        if (x + 1 >= grid.Length || y + 1 >= grid[x].Length)
        {
            return false;
        }

        char tl = grid[x - 1][y - 1];
        char tr = grid[x - 1][y + 1];
        char bl = grid[x + 1][y - 1];
        char br = grid[x + 1][y + 1];

        if ((tl == 'M' && br == 'S') || (tl == 'S' && br == 'M'))
        {
            return (bl == 'S' && tr == 'M') || (bl == 'M' && tr == 'S');
        }

        return false;
    }

    private static bool TopLeftBottomRightDiagonal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0)
        {
            return true;
        }

        if (x >= grid.Length)
        {
            return false;
        }

        if (y >= grid[x].Length)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return TopLeftBottomRightDiagonal(targetChars[1..], grid, x + 1, y + 1);
    }

    private static bool TopRightBottomLeftDiagonal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0)
        {
            return true;
        }

        if (x < 0)
        {
            return false;
        }

        if (y >= grid[x].Length)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return TopRightBottomLeftDiagonal(targetChars[1..], grid, x - 1, y + 1);
    }

    private static bool BottomLeftTopRightDiagonal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0)
        {
            return true;
        }

        if (x >= grid.Length)
        {
            return false;
        }

        if (y < 0)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return BottomLeftTopRightDiagonal(targetChars[1..], grid, x + 1, y - 1);
    }

    private static bool BottomRightTopLeftDiagonal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0)
        {
            return true;
        }

        if (x < 0 || y < 0)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return BottomRightTopLeftDiagonal(targetChars[1..], grid, x - 1, y - 1);
    }

    private static bool LeftRightHorizontal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0) return true;

        if (y >= grid[x].Length)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return LeftRightHorizontal(targetChars[1..], grid, x, y + 1);
    }


    private static bool RightLeftHorizontal(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0) return true;

        if (y < 0)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return RightLeftHorizontal(targetChars[1..], grid, x, y - 1);
    }

    private static bool UpDownVertical(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0) return true;
        if (x >= grid.Length)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return UpDownVertical(targetChars[1..], grid, x + 1, y);
    }

    private static bool DownUpVertical(ArraySegment<char> targetChars, char[][] grid, int x, int y)
    {
        if (targetChars.Count == 0) return true;
        if (x < 0)
        {
            return false;
        }

        var current = targetChars[0];
        var gridChar = grid[x][y];

        if (current != gridChar)
        {
            return false;
        }

        return DownUpVertical(targetChars[1..], grid, x - 1, y);
    }
}