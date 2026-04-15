using System;
using System.Reflection.Metadata;

namespace DocumentGenerator.Core;

public abstract class DocumentExporter
{
    protected abstract IDocumentRenderer CreateRenderer();

    public void Export(DocumentData data)
    {
        var renderer = CreateRenderer();

        string content = renderer.Render(data);

        Console.WriteLine($"\n=== Export Document [{renderer.GetType().Name}] ===");
        Console.WriteLine(content);
        Console.WriteLine("======================================\n");
    }
}

public class HtmlDocumentExporter : DocumentExporter
{
    protected override IDocumentRenderer CreateRenderer()
    {
        return new HtmlRenderer();
    }
}

public class PlainTextDocumentExporter : DocumentExporter
{
    protected override IDocumentRenderer CreateRenderer()
    {
        return new PlainTextRenderer();
    }
}