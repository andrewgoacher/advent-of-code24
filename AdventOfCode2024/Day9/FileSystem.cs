using System.Text;

namespace AdventOfCode2024.Day9;

public class FileSystem
{
    private readonly Allocator _allocator = new();
    private readonly List<Block> _blocks = new();
    private int _length = 0;

    public void Inflate(int[] data)
    {
        var currentId = 0;
        var index = 0;
        for (var i = 0; i < data.Length; i += 2)
        {
            var numberOfBlocks = data[i];
            var block = new Block(currentId, numberOfBlocks, index);
            _blocks.Add(block);

            currentId++;
            index+=numberOfBlocks;

            if (i + 1 >= data.Length)
            {
                break;
            }

            var numberOfFreespaces = data[i + 1];
            _allocator.Allocate(index, numberOfFreespaces);
            index+=numberOfFreespaces;
        }

        _length = index;
    }

    public string Print()
    {
        var array = new long?[_length];
        Array.Fill(array, null);
        foreach (var block in _blocks)
        {
            block.Print(array);
        }

        var sb = new StringBuilder();
        foreach(var item in array)
        {
            if (item == null)
            {
                sb.Append(".");
            }
            else
            {
                sb.Append(item.ToString());
            }
        }

        sb.AppendLine();

        return sb.ToString();
    }

    public void DefragmentSingle()
    {
        for (var i = _blocks.Count - 1; i > 0; --i)
        {
            var block = _blocks[i];
            for (var j = 0; j < block.Length; ++j)
            {
                _allocator.Assign(block, 1, j);
            }
        }
    }

    public void Defragment()
    {
        for (var i = _blocks.Count - 1; i > 0; --i)
        {
            var block = _blocks[i];
            _allocator.Assign(block, block.Length, 0);
        }
    }

    public long Checksum()
    {
        return _blocks.Select(block => block.Checksum()).Sum();
    }
}