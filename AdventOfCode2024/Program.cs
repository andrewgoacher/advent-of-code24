// See https://aka.ms/new-console-template for more information

using AdventOfCode2024;

Console.WriteLine("Hello, World!");

Console.WriteLine("Day 1");
var day1Input = await File.ReadAllLinesAsync("inputs/Day1.txt");
Console.WriteLine($"Part 1: {Day1Solver.Part1(day1Input)}");