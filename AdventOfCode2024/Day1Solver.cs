namespace AdventOfCode2024;

public static class Day1Solver
{
    public static int Part1(string[] input)
    {
        var lines = Collect(input);
        var lhs = lines[0].Order();
        var rhs = lines[1].Order();

        return lhs.Zip(rhs, AbsoluteDistance)
            .Sum();

        static int AbsoluteDistance(int lhs, int rhs) =>
            Math.Abs(lhs - rhs);
    }

    private static int[][] Collect(string[] input)
    {
        var items = new int[2][];
        items[0] = new int[input.Length];
        items[1] = new int[input.Length];

        var lines = input
            .Select(Split)
            .Select(Transform)
            .ToArray();

        for (var i = 0; i < input.Length; i++)
        {
            var (item1, item2) = lines[i];
            items[0][i] = item1;
            items[1][i] = item2;
        }

        return items;

        static (int, int) Transform(string[] line)
        {
            var item1 = int.Parse(line[0]);
            var item2 = int.Parse(line[1]);

            return (item1, item2);
        }

        static string[] Split(string line) =>
            line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    }
}