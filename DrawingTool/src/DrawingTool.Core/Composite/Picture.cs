using System.Collections.Generic;
using DrawingTool.Core.Interfaces;
using DrawingTool.Core.Models;

namespace DrawingTool.Core.Composite;

public class Picture : IShape
{
    private readonly List<IShape> _shapes = new();

    public void Add(IShape shape) => _shapes.Add(shape);
    public void Remove(IShape shape) => _shapes.Remove(shape);

    public void Draw(ICanvas canvas)
    {
        foreach (var shape in _shapes)
        {
            shape.Draw(canvas);
        }
    }

    public void Move(double dx, double dy)
    {
        foreach (var shape in _shapes)
        {
            shape.Move(dx, dy);
        }
    }

    public void Scale(double factor)
    {
        foreach (var shape in _shapes)
        {
            shape.Scale(factor);
        }
    }

    public BoundingBox GetBoundingBox()
    {
        if (_shapes.Count == 0) return new BoundingBox(0, 0, 0, 0);

        double minX = double.MaxValue;
        double minY = double.MaxValue;
        double maxX = double.MinValue;
        double maxY = double.MinValue;

        foreach (var shape in _shapes)
        {
            var bb = shape.GetBoundingBox();
            if (bb.X < minX) minX = bb.X;
            if (bb.Y < minY) minY = bb.Y;
            if (bb.X + bb.Width > maxX) maxX = bb.X + bb.Width;
            if (bb.Y + bb.Height > maxY) maxY = bb.Y + bb.Height;
        }

        return new BoundingBox(minX, minY, maxX - minX, maxY - minY);
    }
}