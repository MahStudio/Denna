using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    class IsdoneToGlyphConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var a = (Status)value;
            string x = "";
            switch (a)
            {
                case Status.notDone:
                    {
                        x = "";
                        break;
                    }
                case Status.Suspended:
                    {
                        x = "";
                        break;
                    }
                case Status.Done:
                    {
                        x = "";
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
