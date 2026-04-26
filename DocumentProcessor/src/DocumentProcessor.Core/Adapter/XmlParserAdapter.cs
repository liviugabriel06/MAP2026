using System.Xml;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Adapter;

public class XmlParserAdapter : IDocumentParser{
    private readonly LegacyXmlParser _legacyParser;

    public XmlParserAdapter(LegacyXmlParser legacyParser){
        _legacyParser = legacyParser;
    }

    public Document Parse(string content){
        var xmlDoc = new XmlDocument();
        try{
            xmlDoc.LoadXml(content);
        }
        catch{
            xmlDoc.LoadXml("<doc><title>Eroare</title><body>XML invalid</body></doc>");
        }

        LegacyDocument legacyDoc = _legacyParser.ParseXml(xmlDoc);

        return new Document{
            Title = legacyDoc.LegacyTitle,
            Content = legacyDoc.LegacyBody
        };
    }
}