using System;
using System.Collections.Generic;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Decorators;

public class CachingDocumentParser : DocumentParserDecorator{
    private readonly Dictionary<string, Document> _cache = new();

    public CachingDocumentParser(IDocumentParser inner) : base(inner) { }

    public override Document Parse(string content){
        if (_cache.TryGetValue(content, out var cachedDoc)){
            Console.WriteLine("[Cache] Documentul gasit in cache. ( fara a parsa din nou )");
            return cachedDoc;
        }

        var doc = _innerParser.Parse(content);

        _cache[content] = doc;

        return doc;
    }
}