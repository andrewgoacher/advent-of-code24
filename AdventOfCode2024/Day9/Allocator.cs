namespace AdventOfCode2024.Day9;

public class Allocator
{
    private readonly List<long> _freespace = new();

    public void Allocate(int start, int size)
    {
        for (var i = 0; i < size; i++)
        {
            _freespace.Add(start + i);
        }
    }

    public void Assign(Block block, int length, int offset = 0)
    {
        if (length > block.Length)
        {
            throw new ArgumentException("Length must be less than the length of the block");
        }

        if (length == 1)
        {
            AssignOffset(block, 0, offset, 1);
            return;
        }

        if (FindContinuousFreespace(length, out var start))
        {
            AssignOffset(block, start, 0, length);
        }
    }

    private void AssignOffset(Block block, int freespaceIndex, int offset, int length)
    {
        var newIndex = _freespace[freespaceIndex];
        if (!block.CanAssign(newIndex, offset))
        {
            return;
        }

        for (var i = 0; i < length; ++i)
        {
            var fsidx = freespaceIndex + i;
            var nidx = _freespace[fsidx];
            var oldIndex = block.Assign(nidx, i+offset);
            _freespace.Add(oldIndex);
        }

        for (var i = 0; i < length; ++i)
        {
            _freespace.RemoveAt(freespaceIndex);
        }

        _freespace.Sort();
    }

    private bool FindContinuousFreespace(int requiredLength, out int start)
    {
        var startIndex = 0;
        var previousIndex = _freespace[0];
        var length = 1;


        for (var i = 1; i < _freespace.Count; ++i)
        {
            var index = _freespace[i];
            if (index - previousIndex > 1)
            {
                startIndex = i;
                length = 1;
            }
            else
            {
                length++;
            }

            previousIndex = index;

            if (length >= requiredLength)
            {
                start = startIndex;
                return true;
            }
        }

        start = -1;
        return false;
    }
}