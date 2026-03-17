using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.models;

namespace TaskManager.Core.Notifications
{
    public class FileLogNotifier : ITaskNotifier
    {
        public void Notify(TaskItem task)
        {
            string logMessage = $"{DateTime.Now}: Task-ul {task.Title} a fost finalizat.\n";

            File.AppendAllText("taasks.log", logMessage);
        }
    }
}
