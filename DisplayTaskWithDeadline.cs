public class TaskItem
{
    public string TaskName { get; set; }
    public bool IsComplete { get; set; }
    public DateTimeOffset Deadline { get; set; }

    // Property to display the task name with deadline time in 12-hour format with AM/PM
    public string DisplayTaskWithDeadline
    {
        get
        {
            return $"{TaskName} (Due: {Deadline:hh:mm tt})";
        }
    }
}
