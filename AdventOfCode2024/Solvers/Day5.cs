namespace AdventOfCode2024.Solvers;

public class Day5 : Solver
{
    private readonly Dictionary<int, List<int>> _replacements = new();

    private int[][] _rows = null!;

    public Day5(string[] input) : base(input)
    {
    }

    public static Day5 FromFile() => new Day5(File.ReadAllLines("inputs/Day5.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        List<int[]> rows = new();

        for (var i = 0; i < inputs.Length; ++i)
        {
            var line = inputs[i];
            if (line.Contains('|'))
            {
                var values = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                var lhs = int.Parse(values[0]);
                var rhs = int.Parse(values[1]);
                if (_replacements.TryGetValue(rhs, out var replacements))
                {
                    replacements.Add(lhs);
                }
                else
                {
                    _replacements.Add(rhs, new List<int> { lhs });
                }

                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var row = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            rows.Add(row);
        }

        _rows = rows.ToArray();
    }

    protected override int Part1()
    {
        return _rows
            .Where(IsInCorrectOrder)
            .Select(GetMidPoint).Sum();
    }

    protected override int Part2()
    {
        return _rows
            .Where(IsNotInCorrectOrder)
            .Select(Reorder)
            .Select(GetMidPoint).Sum();
    }

    private int[] Reorder(int[] row)
    {
        LinkedList<int> linkedList = new(row);

        foreach (var item in row)
        {
            if (!_replacements.TryGetValue(item, out var replacements))
            {
                continue;
            }

            var replacementIndexes = replacements.Where(x => row.Contains(x));
            if (!replacementIndexes.Any())
            {
                continue;
            }
            var last = Last(row, replacementIndexes);
            var currentNode = linkedList.Find(item);
            var theNode = linkedList.Find(last);
            linkedList.Remove(currentNode!);
            linkedList.AddAfter(theNode!, currentNode!);

        }

        return linkedList.ToArray();
    }

    private static int Last(int[] collection, IEnumerable<int> elements)
    {
        int index = -1;
        foreach (var elem in elements)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                var item = collection[i];
                if (item == elem && index < i)
                {
                    index = i;
                }
            }
        }

        return collection[index];
    }

    private bool IsNotInCorrectOrder(int[] row)
    {
        return !IsInCorrectOrder(row);
    }

    private bool IsInCorrectOrder(int[] row)
    {
        for (var i = 0; i < row.Length; ++i)
        {
            var item = row[i];
            if (_replacements.TryGetValue(item, out var replacements))
            {
                var slice = row[i..];
                if (replacements.Exists(x => slice.Contains(x)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int GetMidPoint(int[] row)
    {
        var len = row.Length;
        if (len % 2 == 1)
        {
            len += 1;
        }

        return row[(len / 2) - 1];
    }
}