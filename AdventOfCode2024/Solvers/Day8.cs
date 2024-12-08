﻿using AdventOfCode2024.Day8;

namespace AdventOfCode2024.Solvers;

public class Day8 : Solver
{
    public Day8(string[] input) : base(input)
    {
    }

    public static Day8 FromFile() => new Day8(File.ReadAllLines("inputs/Day8.txt"));

    protected override long Part1()
    {
        Grid grid = new(Input);
        grid.ScanForResonantNodes();

        return grid.ResonantNodes.Count();
    }

    protected override long Part2() => 0;
}