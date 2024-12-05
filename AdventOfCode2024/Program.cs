// See https://aka.ms/new-console-template for more information

using AdventOfCode2024.Solvers;

Console.WriteLine("Hello, World!");

var solvers = new List<Solver>()
{
    Day1.FromFile(),
    Day2.FromFile(),
    Day3.FromFile(),
    Day4.FromFile(),
    Day5.FromFile()
};

var latest = solvers[^1];

Solve(latest);

static void Solve(Solver solver)
{
    Console.WriteLine(solver.GetType().Name);
    var (part1, part2) = solver.Solve();

    Console.WriteLine($"Part 1: {part1}");
    Console.WriteLine($"Part 2: {part2}");
}