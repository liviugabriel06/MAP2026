using System.Xml;
using DocumentProcessor.Core.Models;

namespace DocumentProcessor.Core.Adapter;

public class LegacyXmlParser
{
public LegacyDocument ParseXml(XmlDocument xml)
    {
        string title = xml.SelectSingleNode("//title")?.InnerText ?? "Fără Titlu";
        string body = xml.SelectSingleNode("//body")?.InnerText ?? "Fără Conținut";

        return new LegacyDocument
        {
            LegacyTitle = title,
            LegacyBody = body
        };
    }
}