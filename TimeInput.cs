using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace App1
{
    public class TimeInput
    {
        private TextBox HourInput;
        private TextBox MinuteInput;
        private ComboBox AmPmSelector;

        // Constructor to initialize the UI elements
        public TimeInput(TextBox hourInput, TextBox minuteInput, ComboBox amPmSelector)
        {
            HourInput = hourInput;
            MinuteInput = minuteInput;
            AmPmSelector = amPmSelector;

            // Attach event handlers
            HourInput.TextChanged += HourInput_TextChanged;
            MinuteInput.TextChanged += MinuteInput_TextChanged;
        }

        // Event handler for hour input
        private void HourInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (HourInput.Text.Length == 2)
            {
                //MinuteInput.Focus(FocusState.Programmatic);  // Use explicit FocusState.Programmatic
            }
        }

        // Event handler for minute input
        private void MinuteInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MinuteInput.Text.Length == 2)
            {
                //AmPmSelector.Focus(FocusState.Programmatic);  // Use explicit FocusState.Programmatic
            }
        }
    }
}
