using AdventOfCode2024.Tokenisation;

namespace AdventOfCode2024.Solvers;

public class Day3 : Solver
{
    private IToken[] _tokens = [];
    public Day3(string[] input) : base(input)
    {
    }

    public static Day3 FromFile() => new Day3(new [] { File.ReadAllText("inputs/Day3.txt")});

    protected override void OnPrepare(string[] inputs)
    {
        _tokens = Tokeniser.Tokenise(inputs[0]);
    }

    protected override long Part1()
    {
        return GetMulTokens().Select(token => token.Execute()).Sum();
    }

    protected override long Part2()
    {
        return _tokens.Select(token => token.Execute()).Sum();
    }

    private IEnumerable<MulToken> GetMulTokens()
    {
        foreach (var token in _tokens)
        {
            if (token is MulToken mulToken)
            {
                yield return mulToken;
            }

            if (token is IContainerToken containerToken)
            {
                foreach (var containedToken in containerToken)
                {
                    yield return containedToken;
                }
            }
        }
    }
}