namespace DocumentProcessor.Core.Models;

public class ProcessingResult
{
    public bool IsSuccess {get; set;} 
    public string Message {get; set;} = string.Empty;

    public Document? ProcessedDocument {get; set;}
}