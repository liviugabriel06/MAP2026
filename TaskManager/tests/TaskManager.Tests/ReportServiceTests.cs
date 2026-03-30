using NUnit.Framework;
using System.Linq;
using TaskManager.Core.Models;
using TaskManager.Core.Services;
using TaskManager.Data;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.Tests;

[TestFixture]
public class ReportServiceTests
{
    [Test]
    public void Constructor_AcceptsInMemoryRepository()
    {
        var repository = new InMemoryTaskRepository();
        var reportService = new ReportService(repository);

        Assert.That(reportService, Is.Not.Null);
    }

    [Test]
    public void GenerateSummary_ReturnsCorrectCounts_ForMixedStatuses()
    {
        var repository = new InMemoryTaskRepository();

        repository.Add(new TaskItem { Title = "Task 1", Status = TaskStatus.Done});
        repository.Add(new TaskItem { Title = "Task 2", Status = TaskStatus.ToDo});
        repository.Add(new TaskItem { Title = "Task 3", Status = TaskStatus.Done});

        var reportService = new ReportService(repository);
        var summary = reportService.GenerateSummary();

        Assert.That(summary, Is.EqualTo("Total: 3, Finalizate: 2"));
    }
}