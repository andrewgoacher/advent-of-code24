﻿namespace AdventOfCode2024.Solvers;

public abstract class Solver
{
    private readonly string[] _input;
    protected Solver(string[] input)
    {
        _input = input;
    }

    protected string[] Input => _input;

    public (long, long) Solve()
    {
        OnPrepare(_input);

        var part1 = Part1();
        var part2 = Part2();

        return (part1, part2);
    }

    protected virtual void OnPrepare(string[] inputs)
    {
    }

    protected void Print(string print, bool append = false)
    {
        var user = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var path = Path.Combine(user, "Downloads", $"{this.GetType().Name}.txt");
        if (File.Exists(path) && !append)
        {
            File.Delete(path);
        }

        File.AppendAllText(path, print);
    }

    protected abstract long Part1();
    protected abstract long Part2();
}