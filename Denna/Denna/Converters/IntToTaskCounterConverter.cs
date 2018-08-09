using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class IntToTaskCounterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var count = (int)value;
            var x = "";
            if (count == 0)
                x = "Nothing to do";
            else if (count == 1)
                x = count + " Task to do";
            else
                x = count + " Tasks to do";
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
