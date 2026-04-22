using System;
using DocumentProcessor.Core.Adapter;
using DocumentProcessor.Core.Decorators;
using DocumentProcessor.Core.Interfaces;
using DocumentProcessor.Core.Models;
using DocumentProcessor.Core.Parsers;

namespace DocumentProcessor.Core.Facade;

public class DocumentProcessingFacade
{
    public ProcessingResult  Process(string fileName, string fileContent)
    {
        try
        {
            IDocumentParser baseParser;

            if (fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
            {
                baseParser = new XmlParserAdapter(new LegacyXmlParser());
            }
            else if (fileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                baseParser = new JsonDocumentParser();
            }
            else
            {
                return new ProcessingResult { IsSuccess = false, Message = "Format de fisier necunoscut."};
            }

            IDocumentParser fullyDecoratedParser = new CachingDocumentParser(
                new LoggingDocumentParser(
                    new ValidationDocumentParser(baseParser)
                )
            );

            Document resultDoc = fullyDecoratedParser.Parse(fileContent);

            return new ProcessingResult
            {
                IsSuccess = true,
                Message = $"Document procesat cu succes: {resultDoc.Title}",
                ProcessedDocument = resultDoc
            };
        }
        catch (ValidationException ex)
        {
            return new ProcessingResult { IsSuccess = false, Message = $"Eroare de validare: {ex.Message}" };
        }
        catch (Exception ex)
        {
            return new ProcessingResult { IsSuccess = false, Message = $"Eroare tehnica: {ex.Message}" };
        }
    }
}