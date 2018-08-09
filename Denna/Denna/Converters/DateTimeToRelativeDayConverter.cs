using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    public class DateTimeToRelativeDayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTimeOffset Value = ((DateTimeOffset)value).ToLocalTime() ;
            var Day = Value.Date;
            string x = "";
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
            string Hour = Value.Hour.ToString();
            string Min = Value.Minute.ToString();
            
            x += " " + Hour + ":" + Min;


            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
