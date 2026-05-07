using DrawingTool.Core.Interfaces;
using DrawingTool.Core.Models;

namespace DrawingTool.Core.Shapes;

public class Ellipse : IShape
{
    public double CX { get; private set; }
    public double CY { get; private set; }
    public double RX { get; private set; }
    public double RY { get; private set; }

    public Ellipse(double cx, double cy, double rx, double ry)
    {
        CX = cx; CY = cy; RX = rx; RY = ry;
    }

    public void Draw(ICanvas canvas) => canvas.DrawEllipse(CX, CY, RX, RY);

    public void Move(double dx, double dy)
    {
        CX += dx; CY += dy;
    }

    public void Scale(double factor)
    {
        RX *= factor; RY *= factor;
    }

    public BoundingBox GetBoundingBox() 
        => new BoundingBox(CX - RX, CY - RY, RX * 2, RY * 2);
}