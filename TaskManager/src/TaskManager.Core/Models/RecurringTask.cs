using System;

namespace TaskManager.Core.Models;

public class RecurringTask : TaskItem
{
    public RecurringTask()
    {
        Type = TaskType.Recurring;
    }

    protected override void CompleteCore()
    {
        Status = TaskStatus.Done;

        if (DueDate.HasValue && RecurrenceInterval.HasValue)
            DueDate = DueDate.Value.AddDays(RecurrenceInterval.Value);
    }   
}