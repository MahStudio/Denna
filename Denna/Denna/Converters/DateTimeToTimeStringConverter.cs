using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class DateTimeToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var Value = ((DateTimeOffset)value).ToLocalTime();
            var hour = Value.Hour.ToString();
            var min = Value.Minute.ToString();
            var month = Value.Month;
            var day = Value.Day;
            var x = month + "/" + day + Environment.NewLine + hour + ":" + min;

            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}