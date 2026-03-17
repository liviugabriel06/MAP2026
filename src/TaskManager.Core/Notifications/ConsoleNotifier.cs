using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.models;

namespace TaskManager.Core.Notifications
{
    public class ConsoleNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            Console.WriteLine($"[Consola] Sarcina '{task.Title}' a fost finalizat. ");
        }
    }
}
