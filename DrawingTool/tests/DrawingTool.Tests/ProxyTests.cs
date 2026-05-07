using System;
using NUnit.Framework;
using DrawingTool.Core.Shapes;
using DrawingTool.Core.Proxy;
using DrawingTool.Core.Canvas;

namespace DrawingTool.Tests;

[TestFixture]
public class ProxyTests
{
    [Test]
    public void ReadOnlyProxy_Move_ThrowsInvalidOperationException()
    {
        var circle = new Circle(0, 0, 10);
        var proxy = new ReadOnlyShapeProxy(circle);

        Assert.Throws<InvalidOperationException>(() => proxy.Move(5, 5));
    }

    [Test]
    public void ReadOnlyProxy_Draw_DoesNotThrowAndDelegates()
    {
        var circle = new Circle(0, 0, 10);
        var proxy = new ReadOnlyShapeProxy(circle);
        var canvas = new ConsoleCanvas();

        Assert.DoesNotThrow(() => proxy.Draw(canvas));
    }
}