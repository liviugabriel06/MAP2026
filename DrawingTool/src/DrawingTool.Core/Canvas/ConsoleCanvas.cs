using System;
using DrawingTool.Core.Interfaces;

namespace DrawingTool.Core.Canvas;

public class ConsoleCanvas : ICanvas
{
    public void DrawLine(double x1, double y1, double x2, double y2) 
        => Console.WriteLine($"[Console] Linie: ({x1}, {y1}) -> ({x2}, {y2})");

    public void DrawCircle(double cx, double cy, double r) 
        => Console.WriteLine($"[Console] Cerc: centru({cx}, {cy}), raza={r}");

    public void DrawRect(double x, double y, double w, double h) 
        => Console.WriteLine($"[Console] Dreptunghi: poz({x}, {y}), dim={w}x{h}");

    public void DrawEllipse(double cx, double cy, double rx, double ry) 
        => Console.WriteLine($"[Console] Elipsa: centru({cx}, {cy}), rx={rx}, ry={ry}");
}