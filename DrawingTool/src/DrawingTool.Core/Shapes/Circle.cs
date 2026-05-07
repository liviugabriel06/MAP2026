using DrawingTool.Core.Interfaces;
using DrawingTool.Core.Models;

namespace DrawingTool.Core.Shapes;

public class Circle : IShape
{
    public double CX { get; private set; }
    public double CY { get; private set; }
    public double Radius { get; private set; }

    public Circle(double cx, double cy, double radius)
    {
        CX = cx; CY = cy; Radius = radius;
    }

    public void Draw(ICanvas canvas) => canvas.DrawCircle(CX, CY, Radius);

    public void Move(double dx, double dy)
    {
        CX += dx; CY += dy;
    }

    public void Scale(double factor) => Radius *= factor;

    public BoundingBox GetBoundingBox() 
        => new BoundingBox(CX - Radius, CY - Radius, Radius * 2, Radius * 2);
}