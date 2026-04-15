using System.Runtime.CompilerServices;
using System.Text;

namespace DocumentGenerator.Core;

public interface IDocumentRenderer
{
    string Render(DocumentData data);
}

public class HtmlRenderer : IDocumentRenderer
{
    public string Render(DocumentData data)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"<h1>{data.Title}</h1>");
        sb.AppendLine($"<p><i>Autor: {data.Author} | Data: {data.Date}<i></p>");

        foreach (var section in data.Sections)
        {
            sb.AppendLine($"<div>{section}</div>");
        }

        if (!string.IsNullOrEmpty(data.Footnote))
        sb.AppendLine($"<footer><small>{data.Footnote}</small</footer>");

        return sb.ToString();
    }
}

public class PlainTextRenderer : IDocumentRenderer
{
    public string Render(DocumentData data)
    {
        var sb = new StringBuilder();
        sb.AppendLine("*** {data.Title.ToUpper()} ***");
        sb.AppendLine($"Autor: {data.Author} | Data: {data.Date}");
        sb.AppendLine(new string('-', 30));

        foreach(var section in data.Sections)
        {
            sb.AppendLine(section);
            sb.AppendLine();
        }

        if (!string.IsNullOrEmpty(data.Footnote))
        {
            sb.AppendLine("---");
            sb.AppendLine($"Nota: {data.Footnote}");
        }

        return sb.ToString();
    }
}