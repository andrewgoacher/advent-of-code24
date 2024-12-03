namespace AdventOfCode2024.Tokenisation;

public interface IContainerToken : IToken, IEnumerable<MulToken>
{
    void Add(MulToken token);
}