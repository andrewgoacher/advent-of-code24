namespace AdventOfCode2024.Day8;

public class Grid
{
    private readonly List<Node> _nodes = new();
    private readonly Node?[][] _nodeGrid;

    private readonly HashSet<Vector> _resonantNodes = new();

    public Grid(string[] input)
    {
        _nodeGrid = new Node[input.Length][];

        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y].ToCharArray();
            _nodeGrid[y] = new Node[row.Length];
            for (var x = 0; x < row.Length; x++)
            {
                var item = row[x];
                if (item == '.')
                {
                    continue;
                }

                var node = new Node(new Vector(x, y), item);
                _nodeGrid[y][x] = node;
                _nodes.Add(node);
            }
        }
    }

    public IEnumerable<Vector> ResonantNodes => _resonantNodes;

    public void ScanForResonantNodes()
    {
        var potentials = GatherPotentials(_nodes);
        var validPotentials = potentials.Where(potential => IsValidPotential(potential, _nodeGrid));

        foreach (var potential in validPotentials)
        {
            var headingMul = potential.Heading.Mul(2);
            var sourceResonance = potential.Source.Point.Add(headingMul);
            if (!(sourceResonance.X < 0 || sourceResonance.Y < 0 || sourceResonance.Y >= _nodeGrid.Length ||
                  sourceResonance.X >= _nodeGrid[0].Length))
            {
                _resonantNodes.Add(sourceResonance);
            }

            var targetResonance = potential.Target.Point.Sub(headingMul);
            if (!(targetResonance.X < 0 || targetResonance.Y < 0 || targetResonance.Y >= _nodeGrid.Length ||
                  targetResonance.X >= _nodeGrid[0].Length))
            {
                _resonantNodes.Add(targetResonance);
            }
        }
    }

    private static bool IsValidPotential(Potential potential, Node?[][] nodes)
    {
        var (startX, startY) = potential.Source.Point;
        var (endX, endY) = potential.Target.Point;
        var unit = potential.Heading.Unit();

        int x = startX;
        int y = startY;

        while (x != endX && y != endY)
        {
            if (x == startX && y == startY)
            {
                x += unit.X;
                y += unit.Y;
                continue;
            }

            if (x < 0 || y < 0 || y >= nodes.Length || x >= nodes[y].Length)
            {
                return false;
            }

            var node = nodes[y][x];
            if (node != null)
            {
                return false;
            }

            x += unit.X;
            y += unit.Y;
        }

        return true;
    }

    private static IEnumerable<Potential> GatherPotentials(IEnumerable<Node> nodes)
    {
        var potentials = new List<Potential>();
        var groupedByType = nodes.GroupBy(node => node.NodeType);

        foreach (var group in groupedByType)
        {
            foreach (var node in group)
            {
                foreach (var otherNode in group)
                {
                    if (node == otherNode)
                    {
                        continue;
                    }

                    if (!potentials.Any(potential =>
                        {
                            return (potential.Source == node && potential.Target == otherNode) ||
                                   (potential.Source == otherNode && potential.Target == node);
                        }))
                    {
                        var heading = otherNode.Point.Sub(node.Point);


                        potentials.Add(new Potential(node, otherNode, heading));
                    }
                }
            }
        }

        return potentials;
    }

    record Potential(Node Source, Node Target, Vector Heading);
}