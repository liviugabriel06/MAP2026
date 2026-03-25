namespace TaskManager.Core.Models;

public enum TaskStatus
{
    ToDo,
    InProgress,
    Done
}

public enum TaskPriority
{
    Low = 1,
    Medium = 2,
    High = 3
}

public enum TaskType
{
    Standard,
    Recurring,
    Deadline
}

public enum NotificationType
{
    Email,
    Console, 
    FileLog
}