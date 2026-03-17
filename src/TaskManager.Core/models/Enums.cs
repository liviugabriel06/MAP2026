using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.models
{
    public enum Status { Todo, InProgress, Done }
    public enum Priority { Low = 1, Medium = 2, High = 3 }
    public enum TaskType { Standard, Recurring, Deadline }
    public enum NotificationType { Email, Console, FileLog }
}
