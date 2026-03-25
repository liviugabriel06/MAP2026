using System;
using TaskManager.Core.Models;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.Core.Services;

public class TaskValidator
{
    public void Validate(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("[EROARE] Titlul sarcinii nu poate fi gol.");

        if (task.Title.Length > 200)
            throw new ArgumentException("[EROARE] Titlul nu poate depasi 200 de caractere.");

        if (task.Type == TaskType.Deadline && task is DeadlineTask)
        {
            if (!task.DueDate.HasValue)
                throw new ArgumentException("[EROARE] Sarcina de tip Deadline trebuie sa aiba o data limita (DueDate).");
            
            if (task.DueDate.Value < DateTime.Now && task.Status != TaskStatus.Done)
                throw new ArgumentException("[EROARE] Data limita (DueDate) nu poate fi in trecut pentru o sarcina nefinalizata.");

        }
    }
}