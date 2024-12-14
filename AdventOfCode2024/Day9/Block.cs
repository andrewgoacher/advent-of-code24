using System.Diagnostics;

namespace AdventOfCode2024.Day9;

[DebuggerDisplay("{Id} ({Length})")]
public class Block
{
    private readonly long _id;
    private readonly int _length;
    private readonly long[] _sectors;

    public Block(int id, int length, int start)
    {
        _id = id;
        _length = length;
        _sectors = new long[length];
        for (var i = 0; i < length; i++)
        {
            _sectors[i] = start+i;
        }
    }

    public long Id => _id;
    public int Length => _length;

    public bool CanAssign(long index, int offset)
    {
        var oldIndex = _sectors[offset];
        return oldIndex > index;
    }

    internal long Assign(long index, int offset)
    {
        var oldIndex = _sectors[offset];

        _sectors[offset] = index;

        return oldIndex;
    }

    public void Print(long?[] array)
    {
        foreach (var index in _sectors)
        {
            array[index] = _id;
        }
    }

    public long Checksum()
    {
        return _sectors
            .Select(sector => sector * _id)
            .Sum();
    }
}