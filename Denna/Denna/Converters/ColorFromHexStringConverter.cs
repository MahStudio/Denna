using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Denna.Converters
{
    public class ColorFromHexStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language) => ConvertSolidColorFromHexString(value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
        public static SolidColorBrush ConvertSolidColorFromHexString(string colorStr)
        {

            // create the solidColorbrush
            var myBrush = new SolidColorBrush(GetColor(colorStr));
            return myBrush;
        }
        public static Color GetColor(string colorStr)
        {
            colorStr = colorStr.Replace("#", string.Empty);
            // from #RRGGBB string
            var r = (byte)System.Convert.ToUInt32(colorStr.Substring(0, 2), 16);
            var g = (byte)System.Convert.ToUInt32(colorStr.Substring(2, 2), 16);
            var b = (byte)System.Convert.ToUInt32(colorStr.Substring(4, 2), 16);
            //get the color
            Color color = Color.FromArgb(255, r, g, b);
            return color;
        }
    }
}
