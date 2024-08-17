using Microsoft.UI.Xaml.Data;
using System;

namespace App1
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTime)
            {
                // Format the DateTimeOffset to a readable string format (MM/dd/yyyy)
                return dateTime.ToString("MM/dd/yyyy");
            }
            return string.Empty;  // Return empty string if no date is selected
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
