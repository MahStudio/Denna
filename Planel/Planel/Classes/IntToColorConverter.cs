using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Planel.Classes
{
    class IntToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var gray = new Color();
            gray.A = 255;
            gray.R = 192;
            gray.G = 192;
            gray.B = 192;
            var yelow = new Color();
            yelow.A = 255;
            yelow.R = 255;
            yelow.G = 186;
            yelow.B = 0;
            var a = int.Parse(value.ToString());
            var green = new Color();
            green.A = 255;
            green.R = 32;
            green.G = 200;
            green.B = 165;
            var mycolor = gray;
            if (a == 0)
            {
                mycolor = gray;
            }

            if (a == 1)
            {
                mycolor = yelow;
            }

            if (a == 2)
            {
                mycolor = green;
            }

            var brush = new SolidColorBrush(mycolor);
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var gray = new Color();
            gray.A = 255;
            gray.R = 192;
            gray.G = 192;
            gray.B = 192;
            var yelow = new Color();
            yelow.A = 255;
            yelow.R = 255;
            yelow.G = 186;
            yelow.B = 0;
            var a = int.Parse(value.ToString());
            var green = new Color();
            green.A = 255;
            green.R = 32;
            green.G = 200;
            green.B = 165;
            var mycolor = gray;
            if (a == 0)
            {
                mycolor = gray;
            }

            if (a == 1)
            {
                mycolor = yelow;
            }

            if (a == 2)
            {
                mycolor = green;
            }

            return mycolor;
        }
    }
}