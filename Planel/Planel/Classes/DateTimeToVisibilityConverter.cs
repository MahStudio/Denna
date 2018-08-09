using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Planel.Classes
{
    class DateTimeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var visi = Visibility.Visible;
            try
            {
                var inter = (DateTime)value;
                var now = DateTime.Now;
                var starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
                var endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);

                if (inter >= starttoday && inter <= endtoday)
                {
                    visi = Visibility.Visible;
                }
                else
                {
                    visi = Visibility.Collapsed;
                }
            }
            catch { }

            return visi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var inter = (DateTime)value;
            var now = DateTime.Now;
            var starttoday = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
            var endtoday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);
            var visi = Visibility.Visible;
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