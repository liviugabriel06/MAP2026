using System.Collections.Generic;
using TaskManager.Core.Models;

namespace TaskManager.Core.Interfaces;

public interface ITaskReader
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
}