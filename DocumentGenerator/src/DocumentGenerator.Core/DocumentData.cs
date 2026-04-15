using System.Collections.Generic;

namespace DocumentGenerator.Core;

public class DocumentData
{
    public string Title {get; set;} = string.Empty;
    public string Author {get; set;} = string.Empty;
    public string Date {get; set;} = string.Empty;

    public List<string> Sections {get; set;} = new();

    public string PageFormat {get; set;} = "A4";
    public string Orientation {get; set;} = "Portrait";
    public string Footnote {get; set;} = string.Empty;

    public DocumentData() {}
}