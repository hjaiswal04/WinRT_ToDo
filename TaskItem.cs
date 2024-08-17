using System;

namespace App1
{
    public class TaskItem
    {
        public string TaskName { get; set; }
        public bool IsComplete { get; set; }
        public DateTimeOffset Deadline { get; set; }

        // Property to display the task name with deadline date and time in 12-hour format
        public string DisplayTaskWithDeadline
        {
            get
            {
                // Formats as 'MMM dd, yyyy hh:mm tt' for date and time
                return $"{TaskName} (Due: {Deadline:MMM dd, yyyy hh:mm tt})";
            }
        }
    }
}
