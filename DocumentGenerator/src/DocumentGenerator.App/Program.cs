using System;
using DocumentGenerator.Core;

namespace DocumentGenerator.App;

class Program()
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Sistem de generare documente ===\n");

        var config = AppConfiguration.Instance;
        Console.WriteLine($"[Setari] Director salvare: {config.OutputDirectory}\n");

        var registry = new TemplateRegistry();
        var baseTemplate = new DocumentTemplate { DefaultTitle = "Raport Trimestrial"};
        baseTemplate.PredefinedSections.Add("Continut extras din sablonul clonat ");
        registry.Register("raport_q1", baseTemplate);

        var myClone = registry.Get("raport_q1");

        var docData = new DocumentDataBuilder()
            .WithTitle(myClone.DefaultTitle + " - 2026")
            .ByAuthor("Liviu")
            .OnDate(DateTime.Now.ToShortDateString())
            .WithSection(myClone.PredefinedSections[0])
            .WithSection("Date financiare noi adaugate manual")
            .WithFootnote("== Footer ==")
            .Build();


        IDocumentComponentFactory factory = new ReportComponentFactory();
        var assembler = new DocumentAssembler(factory);
        string assembledContent = assembler.Assemble(docData);

        docData.Sections.Clear();
        docData.Sections.Add(assembledContent);

        DocumentExporter htmlExporter = new HtmlDocumentExporter();
        htmlExporter.Export(docData);

        DocumentExporter textExporter = new PlainTextDocumentExporter();
        textExporter.Export(docData);

        Console.ReadLine();
    }


}