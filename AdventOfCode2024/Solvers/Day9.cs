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

    protected override long Part1()
    {
        var defrag = Defragment();
        var checksum = Checksum(defrag);
        return checksum;
    }

    protected override long Part2()
    {
        return 0;
    }
}