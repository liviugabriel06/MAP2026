using NUnit.Framework;
using System;
using TaskManager.Core.Models;
using TaskManager.Core.Services;

namespace TaskManager.Tests;

[TestFixture]
public class TaskValidatorTests
{
    private TaskValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new TaskValidator();
    }

    [Test]
    public void Validate_EmptyTitle_ThrowsArgumentException()
    {
        var task = new TaskItem { Title = "" };
        Assert.Throws<ArgumentException>(() => _validator.Validate(task));
    }

    [Test]
    public void Validate_TitleExceeds200Chars_ThrowsArgumentException()
    {
        var task = new TaskItem { Title = new string('A', 201) };
        Assert.Throws<ArgumentException>(() => _validator.Validate(task));
    }
}