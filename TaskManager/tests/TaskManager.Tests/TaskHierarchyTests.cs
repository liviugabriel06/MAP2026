using NUnit.Framework;
using System;
using TaskManager.Core.Models;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.Tests;

[TestFixture]
public class TaskHierarchyTests
{
    [TestCase(TaskType.Standard)]
    [TestCase(TaskType.Deadline)]
    [TestCase(TaskType.Recurring)]

    public void Complete_ChangesStatusToDone_ForAllSubtypes(TaskType type)
    {
        TaskItem task = type switch
        {
            TaskType.Deadline => new DeadlineTask { DueDate = DateTime.Now.AddDays(1) },
            TaskType.Recurring => new RecurringTask { DueDate = DateTime.Now, RecurrenceInterval = 5 },
            _ => new TaskItem()
        };

        task.Complete();

        Assert.That(task.Status, Is.EqualTo(TaskStatus.Done));
    }

    [Test]
    public void Complete_WhenAlreadyDone_ThrowsInvalidOperationException()
    {
        var task = new TaskItem();
        task.Complete();

        Assert.Throws<InvalidOperationException> (() => task.Complete());
    }
}