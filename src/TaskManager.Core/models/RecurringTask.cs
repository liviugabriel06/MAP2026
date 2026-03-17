using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.models
{
    public class RecurringTask : TaskItem
    {
        public DateTime DueDate { get; set; }
        public int RecurrenceInterval { get; set; }

        protected override void CompleteCore()
        {
            base.CompleteCore();

            DueDate = DueDate.AddDays(RecurrenceInterval);
        }
    }
}
