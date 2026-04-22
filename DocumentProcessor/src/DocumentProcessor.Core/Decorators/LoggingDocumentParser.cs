using System;
using System.Diagnostics;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Decorators;

public class LoggingDocumentParser : DocumentParserDecorator{
    public LoggingDocumentParser(IDocumentParser inner) : base(inner){ }

    public override Document Parse(string content){
        Console.WriteLine("[Log] Incepem parsarea documentului...");
        var sw = Stopwatch.StartNew();

        var doc = _innerParser.Parse(content);

        sw.Stop();
        Console.WriteLine($"[Log] Parsare finalizata in {sw.ElapsedMilliseconds}ms. Marimea documentului: {doc.Content.Length} caractere.");

        return doc;
    }
}