using Microsoft.UI.Text;
using Microsoft.UI.Xaml.Data;
using System;

namespace App1
{
    public class BooleanToTextDecorationConverter : IValueConverter
    {
        // Converts the IsComplete boolean to a TextDecoration (Strikethrough if true)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isComplete && isComplete)
            {
                return TextDecorations.Strikethrough;
            }
            return TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
