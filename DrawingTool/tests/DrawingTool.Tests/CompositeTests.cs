using NUnit.Framework;
using DrawingTool.Core.Shapes;
using DrawingTool.Core.Composite;

namespace DrawingTool.Tests;

[TestFixture]
public class CompositeTests
{
    [Test]
    public void Picture_Scale_ScalesAllChildren()
    {
        var line = new Line(0, 0, 10, 10);
        var circle = new Circle(5, 5, 5);
        var rect = new Rectangle(0, 0, 20, 10);
        
        var picture = new Picture();
        picture.Add(line);
        picture.Add(circle);
        picture.Add(rect);

        picture.Scale(2.0);

        Assert.That(line.X2, Is.EqualTo(20));
        Assert.That(circle.Radius, Is.EqualTo(10));
        Assert.That(rect.Width, Is.EqualTo(40));
    }

    [Test]
    public void Picture_GetBoundingBox_ReturnsCorrectUnifiedBox()
    {
        var picture = new Picture();
        picture.Add(new Rectangle(0, 0, 10, 10));
        picture.Add(new Circle(20, 20, 5)); 

        var bb = picture.GetBoundingBox();

        Assert.That(bb.X, Is.EqualTo(0));
        Assert.That(bb.Y, Is.EqualTo(0));
        Assert.That(bb.Width, Is.EqualTo(25));
        Assert.That(bb.Height, Is.EqualTo(25));
    }
}