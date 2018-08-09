using System;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class TextToHashtagConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return "#" + (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}