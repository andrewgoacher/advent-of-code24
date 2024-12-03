using System.Text.RegularExpressions;

namespace AdventOfCode2024.Tokenisation;

public static class Tokeniser
{
    private static readonly Regex _tokenRegex = new(@"(?:don't\(\)|do\(\)|mul\((\d+),(\d+)\))");
    public static IToken[] Tokenise(string input)
    {
        var tokens = new List<IToken>();
        IContainerToken? currentToken = null;

        var captures = _tokenRegex.Matches(input);

#pragma warning disable S3267
        foreach (Match match in captures)
#pragma warning restore S3267
        {
            var rawToken = match.Groups[0].Value;

            if (rawToken.StartsWith("mul"))
            {
                var mul = CreateToken(match.Groups[1].Value, match.Groups[2].Value);
                if (currentToken == null)
                {
                    tokens.Add(mul);
                }
                else
                {
                    currentToken.Add(mul);
                }
            }
            else if (rawToken.StartsWith("don't"))
            {
                currentToken = new DontToken();
                tokens.Add(currentToken);
            }
            else if (rawToken.StartsWith("do"))
            {
                currentToken = new DoToken();
                tokens.Add(currentToken);
            }
        }

        return tokens.ToArray();
    }

    private static MulToken CreateToken(string lhs, string rhs)
    {
        return new MulToken()
        {
            LeftHandSide = int.Parse(lhs),
            RightHandSide = int.Parse(rhs)
        };
    }
}