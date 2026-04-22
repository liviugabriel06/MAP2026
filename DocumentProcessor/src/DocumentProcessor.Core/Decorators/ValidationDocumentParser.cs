using System;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Decorators;

public class ValidationException : Exception{
    public ValidationException(string message) : base(message) { } 
}

public class ValidationDocumentParser : DocumentParserDecorator{
    public ValidationDocumentParser (IDocumentParser inner) : base(inner) { }

    public override Document Parse(string content){
        var doc = _innerParser.Parse(content);

        if(string.IsNullOrWhiteSpace(doc.Title))
            throw new ValidationException("Document invalid: Titlul nu poate fi gol.");

        if (string.IsNullOrWhiteSpace(doc.Content) || doc.Content.Length < 10)
            throw new ValidationException("Document invalid: Continutul trebuie sa aiba minim 10 caractere.");

        return doc;
    }
}