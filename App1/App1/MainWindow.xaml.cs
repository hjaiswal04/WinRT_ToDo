using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace App1
{
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<TaskItem> TaskItems { get; } = new ObservableCollection<TaskItem>();
        private TaskItem _selectedTask = null;  // Holds the currently selected task for editing

        public MainWindow()
        {
            this.InitializeComponent();

            // Set the minimum date to today to prevent selecting past dates
            TaskDeadlinePicker.MinDate = DateTimeOffset.Now;

            TaskList.ItemsSource = TaskItems;
        }

        // Method to add or update a task
        private void OnAddUpdateTaskClick(object sender, RoutedEventArgs e)
        {
            string taskName = TaskInput.Text?.Trim();
            DateTimeOffset? selectedDate = TaskDeadlinePicker.Date;
            int hour = int.TryParse(HourInput.Text, out int parsedHour) ? parsedHour : 0;
            int minute = int.TryParse(MinuteInput.Text, out int parsedMinute) ? parsedMinute : 0;
            string amPm = (AmPmSelector.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Adjust hour based on AM/PM selection
            if (amPm == "PM" && hour < 12) hour += 12;
            if (amPm == "AM" && hour == 12) hour = 0; // Handling 12 AM case

            if (selectedDate.HasValue && !string.IsNullOrEmpty(taskName))
            {
                DateTimeOffset deadline = new DateTimeOffset(
                    selectedDate.Value.Year,
                    selectedDate.Value.Month,
                    selectedDate.Value.Day,
                    hour,
                    minute,
                    0,
                    selectedDate.Value.Offset);

                if (_selectedTask != null)
                {
                    // If editing, update the existing task
                    _selectedTask.TaskName = taskName;
                    _selectedTask.Deadline = deadline;
                    _selectedTask = null;  // Reset selected task
                    AddUpdateTaskButton.Content = "Add Task";  // Reset button text to "Add Task"
                }
                else
                {
                    // Add new task to the list
                    TaskItem newTask = new TaskItem { TaskName = taskName, IsComplete = false, Deadline = deadline };
                    TaskItems.Add(newTask);
                }

                // Re-sort the task list by deadline and update task numbers
                SortAndUpdateTasks();

                // Clear the input fields
                ClearInputFields();
            }
        }

        // Method to sort tasks by deadline and update task numbers
        private void SortAndUpdateTasks()
        {
            var sortedTasks = TaskItems.OrderBy(t => t.Deadline).ToList();

            TaskItems.Clear();

            for (int i = 0; i < sortedTasks.Count; i++)
            {
                sortedTasks[i].TaskNumber = $"Task {i + 1}";
                TaskItems.Add(sortedTasks[i]);
            }
        }

        // Method to load the selected task's details into the input fields for editing
        private void OnEditTaskClick(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            TaskItem taskToEdit = editButton.Tag as TaskItem;  // Get the task bound to this button

            if (taskToEdit != null)
            {
                // Load task details into input fields for editing
                TaskInput.Text = taskToEdit.TaskName;
                TaskDeadlinePicker.Date = taskToEdit.Deadline;
                HourInput.Text = taskToEdit.Deadline.Hour > 12 ? (taskToEdit.Deadline.Hour - 12).ToString("00") : taskToEdit.Deadline.Hour.ToString("00");
                MinuteInput.Text = taskToEdit.Deadline.Minute.ToString("00");
                AmPmSelector.SelectedIndex = taskToEdit.Deadline.Hour >= 12 ? 1 : 0;

                // Set the selected task for updating
                _selectedTask = taskToEdit;
                AddUpdateTaskButton.Content = "OK";  // Change button text to "OK" to save the changes
            }
        }

        // Method to delete a task
        private void OnDeleteTaskClick(object sender, RoutedEventArgs e)
        {
            Button deleteButton = sender as Button;
            TaskItem taskToDelete = deleteButton.Tag as TaskItem;  // Get the task bound to this button

            if (taskToDelete != null)
            {
                // Remove the task from the collection
                TaskItems.Remove(taskToDelete);

                // Re-sort the task list and update task numbers after deletion
                SortAndUpdateTasks();
            }
        }

        // Clears input fields after adding or updating a task
        private void ClearInputFields()
        {
            TaskInput.Text = string.Empty;
            TaskDeadlinePicker.Date = null;
            HourInput.Text = string.Empty;
            MinuteInput.Text = string.Empty;
            AmPmSelector.SelectedIndex = 0;
            _selectedTask = null;  // Reset the selected task
        }

        // Event handler for when the user enters the hour value
        private void HourInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HourInput.Text.Length == 2)
            {
                MinuteInput.Focus(FocusState.Programmatic);
            }
        }

        // Event handler for when the user enters the minute value
        private void MinuteInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MinuteInput.Text.Length == 2)
            {
                AmPmSelector.Focus(FocusState.Programmatic);
            }
        }
    }

    // Updated TaskItem class with INotifyPropertyChanged and TaskNumber
    public class TaskItem : INotifyPropertyChanged
    {
        private string _taskName;
        private DateTimeOffset _deadline;
        private bool _isComplete;
        private string _taskNumber;

        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged(nameof(TaskName));
            }
        }

        public DateTimeOffset Deadline
        {
            get { return _deadline; }
            set
            {
                _deadline = value;
                OnPropertyChanged(nameof(Deadline));
                OnPropertyChanged(nameof(DisplayDeadline)); // Notify when deadline changes
            }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                _isComplete = value;
                OnPropertyChanged(nameof(IsComplete));
            }
        }

        public string TaskNumber
        {
            get { return _taskNumber; }
            set
            {
                _taskNumber = value;
                OnPropertyChanged(nameof(TaskNumber));
            }
        }

        // Property to display only the deadline date and time
        public string DisplayDeadline
        {
            get
            {
                return $"Due: {Deadline:MMM dd, yyyy hh:mm tt}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
