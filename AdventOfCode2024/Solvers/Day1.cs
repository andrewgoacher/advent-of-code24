namespace AdventOfCode2024.Solvers;

public class Day1 : Solver
{
    private IEnumerable<int> _lhs = Array.Empty<int>();
    private IEnumerable<int> _rhs = Array.Empty<int>();
    public Day1(string[] input) : base(input)
    {
    }

    public static Day1 FromFile()
    {
        var data = File.ReadAllLines("inputs/Day1.txt");
        return new Day1(data);
    }

    protected override void OnPrepare(string[] inputs)
    {
        var (lhs, rhs) = inputs
            .Select(Split)
            .Select(Transform)
            .Aggregate((new List<int>(), new List<int> ()), (agg, item) =>
            {
                var (lhs, rhs) = agg;
                var (item1, item2) = item;
                lhs.Add(item1);
                rhs.Add(item2);
                return agg;
            });

        _lhs = lhs.Order();
        _rhs = rhs.Order();

        static (int, int) Transform(string[] line)
        {
            var item1 = int.Parse(line[0]);
            var item2 = int.Parse(line[1]);

            return (item1, item2);
        }

        static string[] Split(string line) => line.Split([' '], StringSplitOptions.RemoveEmptyEntries);
    }

    protected override long Part1()
    {
        return _lhs.Zip(_rhs, AbsoluteDisatance)
            .Sum();

        static int AbsoluteDisatance(int lhs, int rhs) => Math.Abs(lhs - rhs);
    }

    protected override long Part2()
    {
        var lhs = CountInstance(_lhs);
        var rhs = CountInstance(_rhs);

        var sum = 0;

        foreach (var (lhsValue, lhsCount) in lhs)
        {
            if (rhs.TryGetValue(lhsValue, out var rhsCount))
            {
                sum += (lhsValue * lhsCount * rhsCount);
            }
        }

        return sum;
    }

    private static IReadOnlyDictionary<int, int> CountInstance(IEnumerable<int> list)
    {
        return list.GroupBy(x => x)
            .ToDictionary(x=>x.Key, x=>x.Count());
    }
}