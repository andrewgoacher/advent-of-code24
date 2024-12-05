namespace AdventOfCode2024.Solvers;

public class Day5 : Solver
{
    private int[][] _rows = null!;
    public Day5(string[] input) : base(input)
    {
    }

    public static Day5 FromFile() => new Day5(File.ReadAllLines("inputs/Day5.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        _rows = new int[inputs.Length][];
        for (var i = 0; i < inputs.Length; ++i)
        {
            _rows[i] = inputs[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    protected override int Part1()
    {
        return _rows.Select(GetMidPoint).Sum();
    }

    protected override int Part2()
    {
        return 0;
    }

    private static int GetMidPoint(int[] row)
    {
        var len = row.Length;
        if (len % 2 == 1)
        {
            len += 1;
        }

        return row[(len / 2)-1];
    }
}