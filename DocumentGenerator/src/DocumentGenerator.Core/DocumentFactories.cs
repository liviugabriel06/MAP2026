namespace DocumentGenerator.Core;

public interface IDocumentComponentFactory
{
    IHeader CreateHeader();
    ISection CreateSection();
    IFooter CreateFooter();
}

public class ReportComponentFactory : IDocumentComponentFactory
{
    public IHeader CreateHeader() => new ReportHeader();
    public ISection CreateSection() => new ReportSection();
    public IFooter CreateFooter() => new ReportFooter();
}

public class InvoiceComponentFactory : IDocumentComponentFactory
{
    public IHeader CreateHeader() => new InvoiceHeader();
    public ISection CreateSection() => new InvoiceSection();
    public IFooter CreateFooter() => new InvoiceFooter();
}