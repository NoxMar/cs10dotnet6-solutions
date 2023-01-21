using System.Xml.Serialization; // XmlSerializer

using static System.Console;
using static System.Environment;
using static System.IO.Path;


List<Shape> listOfShapes = new()
{
  new Circle { Colour = "Red", Radius = 2.5 },
  new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0 },
  new Circle { Colour = "Green", Radius = 8.0 },
  new Circle { Colour = "Purple", Radius = 12.3 },
  new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
};

string path = Combine(CurrentDirectory, "shapes.xml");

XmlSerializer serializerXml = new(typeof(List<Shape>));

{
    using StreamWriter file = File.CreateText(path);
    serializerXml.Serialize(file, listOfShapes);
}

Stream fileXml = File.OpenRead(path);
List<Shape>? loadedShapesXml = serializerXml.Deserialize(fileXml) as List<Shape>;
if (loadedShapesXml is not null)
{
    foreach (Shape item in loadedShapesXml)
    {
        WriteLine("{0} is {1} and has an area of {2:N2}",
        item.GetType().Name, item.Colour, item.Area);
    }
}
else
{
    WriteLine("!!!!ERROR: deserialization unsuccessful");
}

[XmlInclude(typeof(Circle))]
[XmlInclude(typeof(Rectangle))]
public abstract class Shape
{
    public string Colour { get; set; }
    public abstract double Area { get; }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area => (Math.PI * Radius * Radius) / 2;
}

public class Rectangle : Shape
{
    public double Height { get; set; }
    public double Width { get; set; }
    public override double Area => Width * Height;
}