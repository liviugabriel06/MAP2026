using System;

namespace TaskManager.Core.Models;

public class DeadlineTask : TaskItem
{
    public DeadlineTask()
    {
        Type = TaskType.Deadline;
    }

    protected override void CompleteCore()
    {
        Status = TaskStatus.Done;
    }

    protected override bool IsOverdue()
    {
        return DueDate.HasValue && DueDate.Value < DateTime.Now && Status != TaskStatus.Done; 
    }
}