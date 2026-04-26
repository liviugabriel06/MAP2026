using System;
using NUnit.Framework;
using DocumentProcessor.Core.Adapter;
using DocumentProcessor.Core.Decorators;
using DocumentProcessor.Core.Facade;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;
using DocumentProcessor.Core.Parsers;

namespace DocumentProcessor.Tests;

[TestFixture]
public class DocumentProcessorTests
{
    [Test]
    public void Parsers_ReturnDocument_RegardlessOfFormat()
    {
        string xmlContent = "<doc><title>Titlu XML</title><body>Continut XML</body></doc>";
        string jsonContent = "{\"Title\": \"Titlu JSON\", \"Content\": \"Continut JSON\"}";

        IDocumentParser xmlParser = new XmlParserAdapter(new LegacyXmlParser());
        IDocumentParser jsonParser = new JsonDocumentParser();

        Document xmlDoc = xmlParser.Parse(xmlContent);
        Document jsonDoc = jsonParser.Parse(jsonContent);

        Assert.That(xmlDoc.Title, Is.EqualTo("Titlu XML"));
        Assert.That(jsonDoc.Title, Is.EqualTo("Titlu JSON"));
    }

    [Test]
    public void ValidationDecorator_ThrowsExceptation_ForEmptyTitle()
    {
        string badJson = "{\"Title\":\"\",\"Content\":\"Continut suficient de lung\"}";
        IDocumentParser baseParser = new JsonDocumentParser();

        var validator = new ValidationDocumentParser(baseParser);

        Assert.Throws<ValidationException>(() => validator.Parse(badJson));
    }

    private class CallCountingParser : IDocumentParser
    {
        public int CallCount {get; private set;} = 0;
        public Document Parse(string content)
        {
            CallCount++;
            return new Document { Title = "Test", Content = content};
        }
    }

    [Test]
    public  void CachingDecorator_CallsInnerParser_OnlyOnceForSameContent()
    {
        var counterParser = new CallCountingParser();
        var cachingParser = new CachingDocumentParser(counterParser);
        string content = "Acelasi text exact";

        cachingParser.Parse(content);
        cachingParser.Parse(content);

        Assert.That(counterParser.CallCount, Is.EqualTo(1));
    }

    [Test]
    public void Facade_ReturnsIsSuccessFalse_ForInvalidContent_WithoutThrowing()
    {
        var facade = new DocumentProcessingFacade();
        string badJson = "{\"Title\":\"\",\"Content\":\"Prea scurt\"}";

        ProcessingResult result = facade.Process("document.json", badJson);

        Assert.That(result.IsSuccess, Is.False);
        Assert.That(result.Message, Does.Contain("Eroare de validare"));
    }
}