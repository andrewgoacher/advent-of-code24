using System.Diagnostics;

namespace AdventOfCode2024;

[DebuggerDisplay("({X},{Y})")]
public record Vector(int X, int Y)
{
    private Vector Add(int x, int y)
    {
        return new Vector(X + x, Y + y);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public Vector Add(Vector v) => Add(v.X, v.Y);
    public Vector Sub(Vector v) => new Vector(X-v.X, Y-v.Y);
    public Vector Mul(int mul) => new Vector(X * mul, Y * mul);

    public Vector Unit()
    {
        var xModifier = X < 0 ? -1 : 1;
        var yModifier = Y < 0 ? -1 : 1;

        var x = Math.Abs(X);
        var y = Math.Abs(Y);

        if (x == y)
        {
            return new Vector(1*xModifier, 1*yModifier);
        }

        if (x < y)
        {
            if (y % x == 0)
            {
                return new Vector(1*xModifier, (y / x)*yModifier);
            }

            return new Vector(x*xModifier, y*yModifier);
        }

        if (x % y == 0)
        {
            return new Vector((x / y)*xModifier, 1*yModifier);
        }

        return new Vector(x*xModifier, y*yModifier);
    }

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