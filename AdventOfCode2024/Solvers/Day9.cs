using System.Diagnostics;

namespace AdventOfCode2024.Solvers;

public class Day9 : Solver
{
    private int?[] _uncompressed = null!;

    public Day9(string[] input) : base(input)
    {
    }

    public static Day9 FromFile() => new Day9(File.ReadAllLines("inputs/Day9.txt"));

    protected override void OnPrepare(string[] inputs)
    {
        var uncompressedInputs = new List<int?>();
        var rawData = inputs[0].ToCharArray()
            .Select(x => x.ToString())
            .Select(int.Parse).ToArray();

        var currentIndex = 0;
        for (var i = 0; i < rawData.Length; i += 2)
        {
            var numberOfBlocks = rawData[i];

            for (var y = 0; y < numberOfBlocks; y++)
            {
                uncompressedInputs.Add(currentIndex);
            }

            currentIndex += 1;

            if (i + 1 >= rawData.Length)
            {
                continue;
            }

            var numberOfFreeSpaces = rawData[i + 1];
            for (var y = 0; y < numberOfFreeSpaces; y++)
            {
                uncompressedInputs.Add(null);
            }
        }

        _uncompressed = uncompressedInputs.ToArray();
    }

    private int[] Defragment()
    {
        var startIndex = 0;
        var endIndex = _uncompressed.Length - 1;

        var defragging = true;

        var array = new int?[_uncompressed.Length];
        _uncompressed.CopyTo(array, 0);

        while (defragging)
        {
            if (startIndex >= endIndex)
            {
                defragging = false;
                continue;
            }

            var freeBlock = array[startIndex];
            if (freeBlock is not null)
            {
                startIndex += 1;
                continue;
            }

            var blockToMove = array[endIndex];
            if (blockToMove is null)
            {
                endIndex -= 1;
                continue;
            }

            array[startIndex] = blockToMove;
            array[endIndex] = null;
            startIndex += 1;
            endIndex -= 1;
        }


        return array.Where(x => x is not null).Cast<int>().ToArray();
    }

    private int?[] FullyDefragment()
    {
        var endIndex = _uncompressed.Length - 1;

        var defragging = true;
        var array = new int?[_uncompressed.Length];
        _uncompressed.CopyTo(array, 0);

        while (defragging)
        {
            defragging = endIndex > 0;
            if (!defragging)
            {
                break;
            }

            var blockToMove = Block.FromCollection(array, endIndex);
            endIndex = blockToMove.Start - 1;

            var freeSpace = Block.FirstValidPortion(array, blockToMove.Length, endIndex);
            if (freeSpace is null)
            {
                continue;
            }

            for (var i = 0; i < blockToMove.Length; i++)
            {
                var blockIndex = blockToMove.Start + i;
                var freeIndex = freeSpace.Start + i;

                array[freeIndex] = array[blockIndex];
                array[blockIndex] = null;
            }
        }

        /**
         *
         *  00...111...2...333.44.5555.6666.777.888899
            0099.111...2...333.44.5555.6666.777.8888..
            0099.1117772...333.44.5555.6666.....8888..
            0099.111777244.333....5555.6666.....8888..
            00992111777.44.333....5555.6666.....8888..
         */

        return array;
    }

    private static long Checksum(int[] inputs)
    {
        long result = 0;

        for (var i = 0; i < inputs.Length; i++)
        {
            var value = i * inputs[i];
            result += value;
        }

        return result;
    }

    private static long Checksum(int?[] inputs)
    {
        long result = 0;

        for (var i = 0; i < inputs.Length; i++)
        {
            var value = inputs[i];
            if (value is null)
            {
                continue;
            }

            result += (value.Value*i);
        }

        return result;
    }

    protected override long Part1()
    {
        var defrag = Defragment();
        var checksum = Checksum(defrag);
        return checksum;
    }

    protected override long Part2()
    {
        var defrag = FullyDefragment();
        var checksum = Checksum(defrag);
        return checksum;
    }

    class Block
    {
        public int Start { get; private init; }
        public int Length { get; private init; }

        public static Block? FirstValidPortion(int?[] array, int requiredLength, int end)
        {
            int start = -1;
            int length = 0;

            for (var i = 0; i < end; ++i)
            {
                var block = array[i];
                if (block == null)
                {
                    if (start < 0)
                    {
                        start = i;
                        length = 1;
                    }
                    else
                    {
                        length++;
                    }
                }

                else
                {
                    if (length >= requiredLength)
                    {
                        return new Block { Start = start, Length = length };
                    }
                    else
                    {
                        start = -1;
                        length = 0;
                    }
                }
            }

            return null;
        }

        public static Block FromCollection(int?[] array, int start)
        {
            var block = array[start];
            int startIndex = start;
            int length = 1;
            bool keepSearching = true;

            while (keepSearching)
            {
                var nextIndex = startIndex - 1;
                if (nextIndex < 0)
                {
                    keepSearching = false;
                    continue;
                }

                var nextBlock = array[nextIndex];
                if (nextBlock != block)
                {
                    keepSearching = false;
                    continue;
                }

                startIndex = nextIndex;
                length++;
            }

            return new Block() { Start = startIndex, Length = length };
        }
    }
}