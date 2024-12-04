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
        return 0;
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

        if (x <0)
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