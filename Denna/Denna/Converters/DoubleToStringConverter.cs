using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var sx = System.Convert.ToInt32((double)value);
            return sx.ToString() + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}