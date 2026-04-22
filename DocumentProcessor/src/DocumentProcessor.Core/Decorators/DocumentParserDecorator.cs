using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Decorators;

public abstract class DocumentParserDecorator : IDocumentParser{
    protected readonly IDocumentParser _innerParser;

    protected DocumentParserDecorator(IDocumentParser innerParser){
        _innerParser = innerParser;
    }

    public abstract Document Parse(string content);
}