using System.Diagnostics;

namespace AdventOfCode2024.Solvers;

public class Day9 : Solver
{
    private int?[] _uncompressed = null!;
    private Block[] _blocks = null!;

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

        var blocks = new List<Block>();

        var currentIndex = 0;
        for (var i = 0; i < rawData.Length; i += 2)
        {
            var numberOfBlocks = rawData[i];

            for (var y = 0; y < numberOfBlocks; y++)
            {
                uncompressedInputs.Add(currentIndex);
            }

            blocks.Add(new Block() { Id = currentIndex, Length = numberOfBlocks });

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

            if (numberOfFreeSpaces > 0)
            {
                blocks.Add(new Block() { Id = null, Length = numberOfFreeSpaces });
            }
        }

        _uncompressed = uncompressedInputs.ToArray();
        _blocks = blocks.ToArray();
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
        var endIndex = _blocks.Length - 1;

        var defragging = true;
        var array = new Block[_blocks.Length];
        _blocks.CopyTo(array, 0);

        while (defragging)
        {
            defragging = array.Any(a => !a.Touched);
            if (!defragging)
            {
                break;
            }

            var blockToMove = array[endIndex];
            if (blockToMove.Touched)
            {
                endIndex -= 1;
                continue;
            }

            blockToMove.Touched = true;
            if (blockToMove.Id is null)
            {
                endIndex -= 1;
                continue;
            }

            for (var i = 0; i < endIndex; i++)
            {
                var block = array[i];
                if (block.Id is not null)
                {
                    continue;
                }
                if (block.Length >= blockToMove.Length)
                {
                    if (block.Touched)
                    {
                        continue;
                    }
                    block.Touched = true;
                    array[i] = blockToMove;
                    array[endIndex] = block;
                    endIndex -= 1;
                    break;
                }
            }
        }

        var intArray = new int?[array.Sum(x => x.Length)];

        int counter = 0;
        for (var i = 0; i < array.Length; i++)
        {
            var block = array[i];
            for (var j = 0; j < block.Length; j++)
            {
                intArray[counter++] = block.Id;
            }
        }
        //00...111...2...333.44.5555.6666.777.888899
// 00992111777.44.333....5555.6666.....8888..
        return intArray;
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
            var value = i * inputs[i];
            if (value is null)
            {
                continue;
            }

            result += value.Value;
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

    [DebuggerDisplay("{ToString()}")]
    class Block
    {
        public int? Id { get; set; }
        public int Length { get; set; }

        public bool Touched { get; set; } = false;

        public override string ToString()
        {
            if (Id == null)
            {
                return new string('.', Length);
            }

            char x = Id.Value.ToString()[0];
            return new string(x, Length);
        }
    }
}