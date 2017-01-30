using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Planel.Classes
{
    class JsonToWeekdayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string json = value.ToString();
            var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
            string Days = "";
            foreach (var item in toadd)
            {
                Days += item.ToString() + "  ";
            }
            return Days;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string json = value.ToString();
            var toadd = JsonConvert.DeserializeObject<IList<Core.Models.Hobby>>(json);
            return toadd.ToString();
        }
    }
}
