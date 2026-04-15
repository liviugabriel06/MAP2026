using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace DocumentGenerator.Core;

public class DocumentDataBuilder
{
    private string _title = string.Empty;
    private string _author = string.Empty;
    private string _date = DateTime.Now.ToShortDateString();
    private List<string> _sections = new();
    private string _pageFormat = "A4";
    private string _orientation = "Portrait";
    private string _footnote = string.Empty;

    public DocumentDataBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public DocumentDataBuilder ByAuthor(string author)
    {
        _author = author;
        return this;
    }

    public DocumentDataBuilder OnDate(string date)
    {
        _date = date;
        return this;
    }

    public DocumentDataBuilder WithSection(string sectionContent)
    {
        _sections.Add(sectionContent);
        return this;
    }

    public DocumentDataBuilder InLandscape()
    {
        _orientation = "Landscape";
        return this;
    }

    public DocumentDataBuilder WithPageFormat(string format)
    {
        _pageFormat = format;
        return this;
    }

    public DocumentDataBuilder WithFootnote(string footnote)
    {
        _footnote = footnote;
        return this;
    }


    public DocumentData Build()
    {
        if (string.IsNullOrWhiteSpace(_title))
        throw new InvalidOperationException("Titlul este obligatoriu!");
        if (string.IsNullOrWhiteSpace(_author))
            throw new InvalidOperationException("Autorul este oblicatoriu!");
        if (_sections.Count == 0)
            throw new InvalidCastException("Documentul trebuie sa aiba cel putin o sectiune!");

        return new DocumentData
        {
            Title = _title,
            Author = _author,
            Date = _date,
            Sections = new List<string>(_sections),
            PageFormat = _pageFormat,
            Orientation = _orientation,
            Footnote = _footnote
        };

    }

}