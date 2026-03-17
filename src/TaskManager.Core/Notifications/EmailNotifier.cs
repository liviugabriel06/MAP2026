using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.models;

namespace TaskManager.Core.Notifications
{
    public class EmailNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            Console.WriteLine($"Email sent to admin: Task-ul '{task.Title}' a fost finalizat.");
        }
    }
}
