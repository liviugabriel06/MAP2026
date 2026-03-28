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
    public bool WasNotified {get; private set; } = false;

    public void Notify(TaskItem task)
    {
        WasNotified = true;
    }
}

[TestFixture]
public class TaskServiceTests
{
    private TaskService _service;
    private MockNotifier _mockNotifier;

    [SetUp]
    public void Setup()
    {
        var repository = new InMemoryTaskRepository();
        _mockNotifier = new MockNotifier();

        var notifiers = new Dictionary<NotificationType, ITaskNotifier>
        {
            { NotificationType.Console, _mockNotifier }
        };

        _service = new TaskService(repository, notifiers, new TaskValidator());
    }


[Test]
    public void CompleteTask_CallsInjectedNotifier()
    {        
        var task = new TaskItem { Title = "Test Task", NotificationType = NotificationType.Console };
        _service.AddTask(task);
        var addedTask = _service.GetAllTasks().First();

        _service.CompleteTask(addedTask.Id);

        Assert.That(_mockNotifier.WasNotified, Is.True, "Notifier-ul trebuia apelat!");
    }

[Test]
    public void AddTask_IncreasesRepositoryCount()
    {
        var task = new TaskItem { Title = "Test Salvare" };
        _service.AddTask(task);
        var tasks = _service.GetAllTasks();
        Assert.That(tasks.Count(), Is.EqualTo(1));
    }
}