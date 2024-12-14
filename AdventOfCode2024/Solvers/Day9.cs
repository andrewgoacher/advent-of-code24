using System.Diagnostics;
using AdventOfCode2024.Day9;

namespace AdventOfCode2024.Solvers;

public class Day9 : Solver
{
    private int[] _rawData = null!;

    public Day9(string[] input) : base(input)
    {
    }

    public static Day9 FromFile() => new Day9(File.ReadAllLines("inputs/Day9.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        _rawData = inputs[0].ToCharArray()
            .Select(x => x.ToString())
            .Select(int.Parse).ToArray();
    }

    protected override long Part1()
    {
        var filesystem = new FileSystem();
        filesystem.Inflate(_rawData);
        filesystem.DefragmentSingle();

        return filesystem.Checksum();
    }

    protected override long Part2()
    {
        // 6272188436461
        var filesystem = new FileSystem();
        filesystem.Inflate(_rawData);
        filesystem.Defragment();

        return filesystem.Checksum();
    }
}