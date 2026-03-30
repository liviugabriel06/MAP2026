using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Core.Services;
using TaskManager.Data;

namespace TaskManager.Tests;

public class MockNotifier : ITaskNotifier
{
    public bool WasCalled {get; private set; } = false;
    public void Notify(TaskItem task) => WasCalled = true;
}

[TestFixture]
public class TaskServiceTests
{
    private TaskService _service;
    private InMemoryTaskRepository _repositoryl;
    private MockNotifier _mockNotifier;

    [SetUp]
    public void Setup()
    {
        _repositoryl = new InMemoryTaskRepository();
        _mockNotifier = new MockNotifier();

        var notifiers = new Dictionary<NotificationType, ITaskNotifier>
        {
            { NotificationType.Console, _mockNotifier},
            { NotificationType.Email, _mockNotifier},
            { NotificationType.FileLog, _mockNotifier}
        };

        _service = new TaskService(_repositoryl, notifiers, new TaskValidator());
    }

    [Test]
    public void AddTask_IncreasesRepositoryCount()
    {
        var task = new TaskItem { Title = "Test salvare"};
        _service.AddTask(task);

        var tasks = _service.GetAllTasks();
        Assert.That(tasks.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Constructor_RequiresITaskRepository()
    {
        var constructor = typeof(TaskService).GetConstructors().First();
        var hasRepoParameter = constructor.GetParameters().Any(p => p.ParameterType == typeof(ITaskRepository));

        Assert.That(hasRepoParameter, Is.True, "TaskService requires an ITaskRepository parameter");
    }

    [TestCase(NotificationType.Console)]
    [TestCase(NotificationType.Email)]
    [TestCase(NotificationType.FileLog)]
    public void CompleteTask_CallsInjectedNotifier_ForAllTypes(NotificationType tipNotificare)
    {
        var task = new TaskItem { Title = "Task Mock Notifier", NotificationType = tipNotificare};
        _service.AddTask(task);
        var addedTask = _service.GetAllTasks().First();

        _service.CompleteTask(addedTask.Id);

        Assert.That(_mockNotifier.WasCalled, Is.True, $"MockNotifier nu a fost apelat pentru {tipNotificare}!");
    }
}