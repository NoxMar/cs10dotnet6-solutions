public class Circle : Shape
{
    private double radius;
    public Circle(double radius)
    {
        this.radius = radius;
    }

    public double Radius => radius;

    public override double Area => Math.PI * Radius * Radius;
    public override double Width => Radius * 2;
    public override double Height => Radius * 2;
}