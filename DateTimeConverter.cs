using Microsoft.UI.Xaml.Data;
using System;

namespace App1
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                // Format the DateTimeOffset to remove the timezone and show only date and time
                return dateTimeOffset.ToString("MM/dd/yyyy hh:mm tt");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
