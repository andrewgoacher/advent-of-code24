namespace AdventOfCode2024;

public record Vector(int X, int Y)
{
    public Vector Add(int x, int y)
    {
        return new Vector(X + x, Y + y);
    }

    public Vector Add(Vector v) => Add(v.X, v.Y);

    public Vector RotateRight()
    {
        return (X, Y) switch
        {
            (1, 0) => new(0, 1),
            (0, 1) => new(-1, 0),
            (0, -1) => new(1, 0),
            (-1, 0) => new(0, -1),
            _ => throw new InvalidOperationException("Not a valid direction")
        };
    }

    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}