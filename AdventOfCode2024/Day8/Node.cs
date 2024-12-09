using System.Diagnostics;

namespace AdventOfCode2024.Day8;

[DebuggerDisplay("{Point}")]
public class Node
{
    private readonly Vector _point;
    private readonly char _nodeType;
    public Node(Vector point, char nodeType)
    {
        _point = point;
        _nodeType = nodeType;
    }

    public Vector Point => _point;
    public char NodeType => _nodeType;

    public override string ToString()
    {
        return $"{NodeType}: {Point}";
    }
}