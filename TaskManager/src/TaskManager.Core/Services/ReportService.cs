using System.Linq;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services;

public class ReportService
{
    private readonly ITaskReader _reader;
    public ReportService(ITaskReader reader)
    {
        _reader = reader;
    }

    public string GenerateSummary()
    {
        var tasks = _reader.GetAll().ToList();
        var doneCount = tasks.Count(t => t.Status == Models.TaskStatus.Done);

        return $"Total: {tasks.Count}, Finalizate: {doneCount}";
    }
}