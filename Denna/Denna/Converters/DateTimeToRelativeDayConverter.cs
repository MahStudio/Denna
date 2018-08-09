using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    public class DateTimeToRelativeDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var Value = ((DateTimeOffset)value).ToLocalTime();
            var Day = Value.Date;
            var x = "";
            if (Day == DateTime.Today)
                x += "Today";
            else if (Day == DateTime.Today.AddDays(1))
                x += "Tomorrow";
            else if (Day == DateTime.Today.AddDays(-1))
                x += "Yesterday";
            else
            {
                var month = Value.Month;
                var day = Value.Day;
                x += month + "/" + day;
            }

            var hour = Value.Hour.ToString();
            var min = Value.Minute.ToString();

            x += " " + hour + ":" + min;

            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}