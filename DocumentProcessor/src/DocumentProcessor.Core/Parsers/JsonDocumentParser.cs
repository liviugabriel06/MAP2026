using System.Text.Json;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Parsers;

public class JsonDocumentParser : IDocumentParser{
    public Document Parse(string content){
        try{
            var doc = JsonSerializer.Deserialize<Document>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return doc ?? new Document();
        }
        catch{
            return new Document { Title = "Eroare", Content = "JSON invalid" };
        }
    }
}