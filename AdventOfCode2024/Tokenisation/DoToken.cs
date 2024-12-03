using System.Collections;

namespace AdventOfCode2024.Tokenisation;

public class DoToken : IContainerToken
{
    private readonly List<MulToken> _tokens = new();

    public void Add(MulToken token)
    {
        _tokens.Add(token);
    }
    public int Execute()
    {
        return _tokens.Select(token => token.Execute()).Sum();
    }

    public IEnumerator<MulToken> GetEnumerator() => _tokens.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}