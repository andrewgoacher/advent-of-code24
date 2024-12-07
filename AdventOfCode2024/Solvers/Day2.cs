namespace AdventOfCode2024.Solvers;

public class Day2 : Solver
{
    private int[][] _reports = [];

    public Day2(string[] input) : base(input)
    {
    }

    public static Day2 FromFile()
    {
        var data = File.ReadAllLines("inputs/Day2.txt");
        return new(data);
    }

    protected override void OnPrepare(string[] inputs)
    {
        _reports = new int[inputs.Length][];
        for (var i = 0; i < inputs.Length; i++)
        {
            var line = inputs[i];
            var items = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
            _reports[i] = items.Select(int.Parse).ToArray();
        }
    }

    protected override long Part1()
    {
        return _reports
            .Count(report => IsIncreasing(report) || IsDecreasing(report));
    }

    protected override long Part2()
    {
        return _reports
            .Count(report => IsIncreasing(report, true) || IsDecreasing(report, true));
    }

    private static bool IsValid(int[] items)
    {
        for (var i = 1; i < items.Length; i++)
        {
            var previous = items[i - 1];
            var current = items[i];
            var diff = current - previous;
            if (diff < 1 || diff > 3)
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsIncreasing(int[] collection, bool allowSafeRemove = false)
    {
        if (IsValid(collection))
        {
            return true;
        }

        if (!allowSafeRemove)
        {
            return false;
        }


        for (var i = 0; i < collection.Length; i++)
        {
            var l = new List<int>(collection);
            l.RemoveAt(i);
            if (IsValid(l.ToArray()))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsDecreasing(int[] collection, bool allowSafeRemove = false)
    {
        return IsIncreasing(collection.Reverse().ToArray(), allowSafeRemove);
    }
}