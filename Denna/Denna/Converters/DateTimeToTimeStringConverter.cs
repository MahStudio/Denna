using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class DateTimeToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            DateTimeOffset Value = (DateTimeOffset)value;
            string Hour = Value.Hour.ToString();
            string Min = Value.Minute.ToString();
            var month = Value.Month;
            var day = Value.Day;
            string x = month + "/" + day + Environment.NewLine + Hour + ":" + Min;


            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
