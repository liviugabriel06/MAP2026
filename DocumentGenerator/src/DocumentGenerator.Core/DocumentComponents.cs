namespace DocumentGenerator.Core;

public interface IHeader
{
    string GetHeaderContent();
}

public interface ISection
{
    string GetSectionContent(string text);
}

public interface IFooter
{
    string GetFooterContent();
}

public class ReportHeader : IHeader
{
    public string GetHeaderContent() => "== Raport Oficial ==\n";
}

public class ReportSection : ISection
{
    public string GetSectionContent(string text) => $"[Sectiune Date] {text}\n";
}

public class ReportFooter : IFooter
{
    public string GetFooterContent() => "\n == Sfarsitul raportului ==\n";
}

public class InvoiceHeader : IHeader
{
    public string GetHeaderContent() => "== Factura ==\n";
}

public class InvoiceSection : ISection
{
    public string GetSectionContent(string text) => $"[Articol] {text} ... [Pret]\n";
}

public class InvoiceFooter : IFooter
{
    public string GetFooterContent() => "\n == Termen de plata: 30 de zile == \n";
}