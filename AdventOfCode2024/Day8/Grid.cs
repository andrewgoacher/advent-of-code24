using System.Text;

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

    public void PrintGrid()
    {
        var usr = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var file = "grid.txt";

        var path = Path.Combine(usr, "Downloads", file);

        var gridRaw = new char[_nodeGrid.Length][];

        for (var y = 0; y < _nodeGrid.Length; y++)
        {
            gridRaw[y] = new char[_nodeGrid[y].Length];
            for (var x = 0; x < _nodeGrid[y].Length; x++)
            {
                var node = _nodeGrid[y][x];
                char nodeChar = node == null ? '.' : node.NodeType;
                gridRaw[y][x] = nodeChar;
            }
        }

        foreach (var resonance in _resonantNodes)
        {
            var (x, y) = resonance;
            var node = _nodeGrid[y][x];
            if (node != null)
            {
                continue;
            }
            gridRaw[y][x] = '#';
        }

        var sb = new StringBuilder();

        for (var y = 0; y < _nodeGrid.Length; y++)
        {
            for (var x = 0; x < _nodeGrid[y].Length; x++)
            {
                sb.Append(gridRaw[y][x]);
            }
            sb.AppendLine();
        }

        File.WriteAllText(path, sb.ToString());
    }

    public IEnumerable<Vector> ResonantNodes => _resonantNodes;

    public void ScanForResonantNodes(bool continueScanning = false)
    {
        _resonantNodes.Clear();
        var potentials = GatherPotentials(_nodes);
        var validPotentials = potentials.Where(potential => IsValidPotential(potential, _nodeGrid));

        foreach (var potential in validPotentials)
        {
            if (!continueScanning)
            {
                ScanForResonantNodesForPotentialSingle(potential.Source.Point, potential.Target.Point, potential.Heading);
            }
            else
            {
                ScanForResonantNodesForPotentialContinuous(potential.Source.Point, potential.Target.Point, potential.Heading);
            }
        }
    }

    private void ScanForResonantNodesForPotentialContinuous(Vector source, Vector target, Vector headingMul)
    {
        bool continueSource = true;
        bool continueTarget = true;

        _resonantNodes.Add(source);
        _resonantNodes.Add(target);

        Vector s = source;
        Vector t = target;

        while (continueSource || continueTarget)
        {
            var sourceResonance = s.Sub(headingMul);
            if (!(sourceResonance.X < 0 || sourceResonance.Y < 0 || sourceResonance.Y >= _nodeGrid.Length ||
                  sourceResonance.X >= _nodeGrid[0].Length))
            {
                _resonantNodes.Add(sourceResonance);
            }
            else
            {
                continueSource = false;
            }

            var targetResonance = t.Add(headingMul);
            if (!(targetResonance.X < 0 || targetResonance.Y < 0 || targetResonance.Y >= _nodeGrid.Length ||
                  targetResonance.X >= _nodeGrid[0].Length))
            {
                _resonantNodes.Add(targetResonance);
            }
            else
            {
                continueTarget = false;
            }

            s = sourceResonance;
            t = targetResonance;
        }
    }

    private void ScanForResonantNodesForPotentialSingle(Vector source, Vector target, Vector headingMul)
    {
        var sourceResonance = source.Sub(headingMul);
        if (!(sourceResonance.X < 0 || sourceResonance.Y < 0 || sourceResonance.Y >= _nodeGrid.Length ||
              sourceResonance.X >= _nodeGrid[0].Length))
        {
            _resonantNodes.Add(sourceResonance);
        }

        var targetResonance = target.Add(headingMul);
        if (!(targetResonance.X < 0 || targetResonance.Y < 0 || targetResonance.Y >= _nodeGrid.Length ||
              targetResonance.X >= _nodeGrid[0].Length))
        {
            _resonantNodes.Add(targetResonance);
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