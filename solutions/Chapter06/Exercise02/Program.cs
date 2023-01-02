using static System.Console;

string shapeDescriptionFormat = "{0} H: {1}, W: {2}, Area: {3}";
Rectangle r = new(height: 3, width: 4.5);
WriteLine(shapeDescriptionFormat, "Rectangle", r.Height, r.Width, r.Area);
Square s = new(5);
WriteLine(shapeDescriptionFormat, "Square", s.Height, s.Width, s.Area);
Circle c = new(radius: 2.5);
WriteLine(shapeDescriptionFormat, "Circle", c.Height, c.Width, c.Area);