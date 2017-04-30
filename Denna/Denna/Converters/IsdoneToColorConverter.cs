using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Denna.Converters
{
    class IsdoneToColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int a = (int)value;
            SolidColorBrush x = new SolidColorBrush(Colors.Red);
            switch (a)
            {
                case 0:
                    {
                       x= (SolidColorBrush)Application.Current.Resources["GrayBrush"];
                        break; 
                    }
                case 1:
                    {
                        x = (SolidColorBrush)Application.Current.Resources["YellowBrush"];
                        break;
                    }
                case 2:
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
