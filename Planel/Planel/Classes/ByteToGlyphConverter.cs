using System;
using Windows.UI.Xaml.Data;

namespace Planel.Classes
{
    class ByteToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var a = (byte)int.Parse(value.ToString());
            var ret = "";
            // 
            if (a == 0)
                ret = "";
            if (a == 1)
                ret = "";
            if (a == 2)
                ret = "";
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var a = (byte)int.Parse(value.ToString());
            var ret = "";
            if (a == 0)
                ret = "";
            if (a == 1)
                ret = "";
            if (a == 2)
                ret = "";
            return ret;
        }
    }
}