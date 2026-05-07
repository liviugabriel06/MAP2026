using NUnit.Framework;
using DrawingTool.Core.Shapes;
using DrawingTool.Core.Canvas;

namespace DrawingTool.Tests;

[TestFixture]
public class BridgeTests
{
    [Test]
    public void SvgCanvas_DrawCircle_GeneratesCorrectSvgTag()
    {
        var circle = new Circle(10, 10, 5);
        var canvas = new SvgCanvas();

        circle.Draw(canvas);
        string svgOutput = canvas.GetSvg();

        Assert.That(svgOutput, Does.Contain("<circle"));
        Assert.That(svgOutput, Does.Contain("cx=\"10\""));
        Assert.That(svgOutput, Does.Contain("r=\"5\""));
    }
}