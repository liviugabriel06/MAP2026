using System;
using DrawingTool.Core.Composite;
using DrawingTool.Core.Canvas;
using DrawingTool.Core.Shapes;

var canvas = new SvgCanvas();
var mainPicture = new Picture();

mainPicture.Add(new Rectangle(10, 10, 100, 50));

var nestedPicture = new Picture();
nestedPicture.Add(new Circle(60, 35, 20));
nestedPicture.Add(new Line(10, 10, 110, 60));

mainPicture.Add(nestedPicture);

mainPicture.Draw(canvas);
string svgOutput = canvas.GetSvg();

Console.WriteLine("=== REZULTATUL SVG GENERAT DE APLICATIA TA ===");
Console.WriteLine(svgOutput);