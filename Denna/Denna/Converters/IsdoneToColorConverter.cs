using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Denna.Converters
{
    class IsdoneToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var a = (int)value;
            var x = new SolidColorBrush(Colors.Red);
            switch (a)
            {
                case 2:
                    {
                        x = (SolidColorBrush)Application.Current.Resources["BlueBrush"];
                        break;
                    }
                case 1:
                    {
                        x = (SolidColorBrush)Application.Current.Resources["YellowBrush"];
                        break;
                    }
                case 0:
                    {
                        x = (SolidColorBrush)Application.Current.Resources["GreenBrush"];
                        break;
                    }
                default:
                    break;
            }

            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
