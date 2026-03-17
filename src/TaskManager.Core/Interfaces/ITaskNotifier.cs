using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.models;

namespace TaskManager.Core.Interfaces
{
    public interface ITaskNotifier
    {
        void Notify(TaskItem task);
    }
}
