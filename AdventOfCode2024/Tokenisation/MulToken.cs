namespace AdventOfCode2024.Tokenisation;

public class MulToken : IToken
{
    public required int LeftHandSide { get; init; }
    public required int RightHandSide { get; init; }

    public int Execute()
    {
        return LeftHandSide * RightHandSide;
    }
}