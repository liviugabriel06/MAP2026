using System.Collections.Generic;
using DocumentGenerator.Core;

namespace DocumentGenerator.Core;

public class TemplateRegistry
{
    private readonly Dictionary<string, DocumentTemplate> _prototypes = new();

    public void Register(string key, DocumentTemplate template)
    {
        _prototypes[key] = template;
    }

    public DocumentTemplate Get(string key)
    {
        if (!_prototypes.TryGetValue(key, out var proto))
            throw new KeyNotFoundException($"Șablonul cu cheia '{key}' nu a fost găsit!");

        return (DocumentTemplate)proto.Clone();
    }
}