using System;

namespace TaskManager.Core.Models;

public class TaskItem
{
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public TaskStatus Status {get; set;} = TaskStatus.ToDo;
    public TaskPriority Priority {get; set;} = TaskPriority.Medium;
    public TaskType Type {get; set;} = TaskType.Standard;
    public NotificationType NotificationType {get; set;} = NotificationType.Console;
    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public virtual DateTime? DueDate {get; set;}
    public virtual int? RecurrenceInterval {get; set;}

    public void Complete()
    {
        if (Status == TaskStatus.Done)
            throw new InvalidOperationException("Sarcina este deja finalizata.");
        
        CompleteCore();

        if (Status != TaskStatus.Done)
            throw new InvalidOperationException("Statusul sarcinii trebuie sa fie 'Done'.");
        
        if (Status == TaskStatus.Done && IsOverdue())
            throw new InvalidOperationException("O sarcina nu poate fi 'Done' si 'Overdue' in acelasi timp.");
    }
        
        protected virtual void CompleteCore()
        {
            Status = TaskStatus.Done;
        }

        protected virtual bool IsOverdue()
        {
            return false;
        }
    }