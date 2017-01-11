using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Planel.Classes
{
    class DateTimeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime inter = (DateTime)value;
            DateTime now = DateTime.Now;
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            Visibility visi = Visibility.Visible;
                if (inter >= starttoday && inter <= endtoday)
            {
                visi = Visibility.Visible;
            }
                else
            {
                visi = Visibility.Collapsed;
            }
            return visi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            DateTime inter = (DateTime)value;
            DateTime now = DateTime.Now;
            DateTime starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            DateTime endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            Visibility visi = Visibility.Visible;
            if (inter >= starttoday && inter <= endtoday)
            {
                visi = Visibility.Visible;
            }
            else
            {
                visi = Visibility.Collapsed;
            }
            return visi;
        }
    }
}
