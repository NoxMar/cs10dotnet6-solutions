namespace Packt.Shared;

public struct DisplacementVector
{
    public int X;
    public int Y;
    public DisplacementVector(int initialX, int initialY)
    {
        X = initialX;
        Y = initialY;
    }

    public static DisplacementVector operator +(
        DisplacementVector v1,
        DisplacementVector v2
    )
    {
        return new(v1.X + v2.X, v1.Y + v2.Y);
    }
}