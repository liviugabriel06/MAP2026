using System.Collections.Generic;
using System.Linq;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Data;

public class InMemoryTaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public IEnumerable<TaskItem> GetAll() => _tasks.ToList();

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public void Add(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
    }

    public void Update(TaskItem task)
    {
        var index = _tasks.FindIndex(t => t.Id == task.Id);
        if (index != -1)
            _tasks[index] = task;
    }

    public void Delete(int id)
    {
        var task = GetById(id);
        if (task != null)
            _tasks.Remove(task);
    }
}