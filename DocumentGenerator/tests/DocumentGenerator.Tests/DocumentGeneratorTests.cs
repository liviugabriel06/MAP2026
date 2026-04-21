using System;
using NUnit.Framework;
using DocumentGenerator.Core;
using NUnit.Framework.Constraints;

namespace DocumentGenerator.Tests;

[TestFixture]
public class DocumentGeneratorTests
{
    [Test]
    public void Export_ProducesDifferentOutputs_ForDifferentExporters()
    {
        var data = new DocumentData { Title = "Test", Author = "Autor"};
        data.Sections.Add("Continut test");

        var htmlExporter = new HtmlDocumentExporter();
        var textExporter = new PlainTextDocumentExporter();

        IDocumentRenderer htmlRenderer = new HtmlRenderer();
        IDocumentRenderer textRenderer = new PlainTextRenderer();

        string htmlOutput = htmlRenderer.Render(data);
        string textOutput = textRenderer.Render(data);

        Assert.That(htmlOutput, Is.Not.EqualTo(textOutput), "Formatele de export ar trebui sa fie diferite.");
    }

    [Test]
    public void Assembler_UsesCorrectFactoryParts_ForReportAndInvoice()
    {
        var data = new DocumentData { Title = "Test" };
        data.Sections.Add("Continut");

        var reportAssembler = new DocumentAssembler(new ReportComponentFactory());
        var invoiceAssembler = new DocumentAssembler(new InvoiceComponentFactory());

        string reportOutput = reportAssembler.Assemble(data);
        string invoiceOutput = invoiceAssembler.Assemble(data);

        Assert.That(reportOutput, Does.Contain("== Raport Oficial =="), "Raportul trebuie sa aiba antetul de raport.");
        Assert.That(invoiceOutput, Does.Contain("== Factura =="), "Factura trebuie sa aiba antetul de factura.");
    }

    [Test]
    public void Builder_ThrowsException_WhenTitleIsMissing()
    {
        var builder = new DocumentDataBuilder()
            .ByAuthor("Liviu")
            .WithSection("O sectiune");

        Assert.Throws<InvalidOperationException>(() => builder.Build());
    }

    [Test]
    public void Prototype_ClonesAreIndependent_WhenModifyingCollections()
    {
        var registry = new TemplateRegistry();
        var template = new DocumentTemplate { DefaultTitle = "Sablon de baza" };
        template.PredefinedSections.Add("Sectiune 1");

        registry.Register("baza", template);

        var clona1 = registry.Get("baza");
        var clona2 = registry.Get("baza");

        clona1.PredefinedSections.Add("Sectiunea 2 - Adaugare doar in clona 1");

        Assert.That(clona1.PredefinedSections.Count, Is.EqualTo(2));
        Assert.That(clona2.PredefinedSections.Count, Is.EqualTo(1), "Listere se suprascriu! Clonarea a esuat.");
    }
}