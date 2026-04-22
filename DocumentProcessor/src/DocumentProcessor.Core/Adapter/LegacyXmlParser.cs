using System.Xml;
using DOcumentProcessor.Core.Models;

namespace DOcumentProcessor.Core.Adapter;

public class LegacyXmlParser{
    public LegacyXmlParser(XmlDocument xml){
        string title = xml.SelectSingleNode("//title")?.InnerText ?? "Fara titlu";
        string body = xml.SelectSingleNode("//body")?.InnerText ?? "Fara continut";

        return new LegacyDocument{
            LegacyTitle = title;
            LegacyBody = body;
        }

    }
}