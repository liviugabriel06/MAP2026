using System;
using System.Collections.Generic;

namespace DocumentGenerator.Core;

public class FormattingSettings : ICloneable
{
    public string PageFormat {get; set;} = "A4";
    public string Orientation {get; set;} = "Portrait";

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}

public class DocumentTemplate : ICloneable
{
    public string DefaultTitle {get; set;} = string.Empty;
    public List<string> PredefinedSections {get; set;} = new();
    public FormattingSettings Formatting {get; set;} = new();

    public object Clone()
    {
        return new DocumentTemplate
        {
            DefaultTitle = this.DefaultTitle,
            PredefinedSections = new List<string>(this.PredefinedSections),
            Formatting = (FormattingSettings)this.Formatting.Clone()
        };
    }
}