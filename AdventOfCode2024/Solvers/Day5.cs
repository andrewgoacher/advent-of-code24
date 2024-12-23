﻿
namespace AdventOfCode2024.Solvers;

public class Day5 : Solver
{

    private readonly List<(int, int)> _beforeAfter = new();
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
                _beforeAfter.Add((lhs, rhs));

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

    protected override long Part1()
    {
        return _rows
            .Where(IsInCorrectOrder)
            .Select(GetMidPoint).Sum();
    }

    protected override long Part2()
    {
        return _rows
            .Where(IsNotInCorrectOrder)
            .Select(Reorder)
            .Select(GetMidPoint).Sum();
    }

    private int[] Reorder(int[] collection)
    {
        if (IsInCorrectOrder(collection))
        {
            return collection;
        }
        LinkedList<int> linkedList = new(collection);
        var arr = linkedList.ToArray();

        for(var i = 0;i<arr.Length;++i)
        {
            var item = arr[i];

            var mustBeAfter = _beforeAfter.Where(x => x.Item1 == item)
                .Select(x => x.Item2)
                .Where(x => arr.Contains(x));

            if (!mustBeAfter.Any())
            {
                continue;
            }

            var earliestIndex = mustBeAfter.Select(x => IndexOf(linkedList.ToArray(), x)).OrderBy(x => x).First();
            var earliestItem = arr[earliestIndex];

            var currentNode = linkedList.Find(item);
            var theNode = linkedList.Find(earliestItem);
            linkedList.Remove(currentNode!);
            linkedList.AddBefore(theNode!, currentNode!);
            arr = linkedList.ToArray();
        }

        return Reorder(arr);
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
            var mustBeAfter = _beforeAfter.Where(x => x.Item1 == item)
                .Select(x => x.Item2);

            foreach (var a in mustBeAfter)
            {
                var idx = IndexOf(row, a);
                if (idx == -1)
                {
                    continue;
                }
                if (i > idx)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static int IndexOf(int[] collection, int element)
    {
        int index = -1;

        for (var i = 0; i < collection.Length; ++i)
        {
            var item = collection[i];
            if (item == element)
            {
                index = i;
            }
        }

        return index;
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