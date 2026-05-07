using System.Text;
using DrawingTool.Core.Interfaces;

namespace DrawingTool.Core.Canvas;

public class SvgCanvas : ICanvas
{
    private readonly StringBuilder _sb = new();

    public void DrawLine(double x1, double y1, double x2, double y2) 
        => _sb.AppendLine($"  <line x1=\"{x1}\" y1=\"{y1}\" x2=\"{x2}\" y2=\"{y2}\" stroke=\"black\" />");

    public void DrawCircle(double cx, double cy, double r) 
        => _sb.AppendLine($"  <circle cx=\"{cx}\" cy=\"{cy}\" r=\"{r}\" fill=\"none\" stroke=\"black\" />");

    public void DrawRect(double x, double y, double w, double h) 
        => _sb.AppendLine($"  <rect x=\"{x}\" y=\"{y}\" width=\"{w}\" height=\"{h}\" fill=\"none\" stroke=\"black\" />");

    public void DrawEllipse(double cx, double cy, double rx, double ry) 
        => _sb.AppendLine($"  <ellipse cx=\"{cx}\" cy=\"{cy}\" rx=\"{rx}\" ry=\"{ry}\" fill=\"none\" stroke=\"black\" />");

    public string GetSvg() => $"<svg xmlns=\"http://www.w3.org/2000/svg\">\n{_sb}</svg>";
}