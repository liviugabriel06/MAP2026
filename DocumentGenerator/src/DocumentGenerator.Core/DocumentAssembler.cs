using System.Text;

namespace DocumentGenerator.Core;

public class DocumentAssembler
{
    private readonly IDocumentComponentFactory _factory;

    public DocumentAssembler(IDocumentComponentFactory factory)
    {
        _factory = factory;
    }

    public string Assemble(DocumentData data)
    {
        var sb = new StringBuilder();

        var header = _factory.CreateHeader();
        sb.AppendLine(header.GetHeaderContent());

        var sectionComponent = _factory.CreateSection();
        foreach(var text in data.Sections)
        {
            sb.AppendLine(sectionComponent.GetSectionContent(text));
        }

        var footer = _factory.CreateFooter();
        sb.AppendLine(footer.GetFooterContent());

        return sb.ToString();
    }
}