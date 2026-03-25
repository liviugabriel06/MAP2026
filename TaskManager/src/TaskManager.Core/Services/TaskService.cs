using System;
using System.Collections.Generic;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;
    private readonly IReadOnlyDictionary<NotificationType, ITaskNotifier> _notifiers;
    private readonly TaskValidator _validator;


    public TaskService(
        ITaskRepository repository,
        IReadOnlyDictionary<NotificationType, ITaskNotifier> notifiers,
        TaskValidator validator)
    {
        _repository = repository;
        _notifiers = notifiers;
        _validator = validator;
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return _repository.GetAll();
    }

    public void AddTask(TaskItem task)
    {
        _validator.Validate(task);
        _repository.Add(task);
    }

    public void UpdateTask(TaskItem task)
    {
        _validator.Validate(task);
        _repository.Update(task);
    }

    public void DeleteTask(int id)
    {
        _repository.Delete(id);
    }

    public void CompleteTask(int id)
    {
        var task = _repository.GetById(id);
        if (task == null)
            throw new ArgumentException("Sarcina nu a fost gasita.");
        
        task.Complete();
        _repository.Update(task);

        if (_notifiers.TryGetValue(task.NotificationType, out var notifier))
            notifier.Notify(task);
    }
}