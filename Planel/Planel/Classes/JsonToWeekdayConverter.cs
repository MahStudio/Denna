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
            if (toadd.Count == 7)
            {
                Days = MultilingualHelpToolkit.GetString("Everydat", "Text"); ;
            }
            else
            {
                foreach (var item in toadd)
                {
                    string x = null;
                    if(item== DayOfWeek.Friday)
                    {
                       x= MultilingualHelpToolkit.GetString("fri", "Content");
                    }
                    if (item == DayOfWeek.Saturday)
                    {
                       x= MultilingualHelpToolkit.GetString("sat", "Content");
                    }
                    if (item == DayOfWeek.Sunday)
                    {
                       x= MultilingualHelpToolkit.GetString("sun", "Content");
                    }
                    if (item == DayOfWeek.Monday)
                    {
                       x= MultilingualHelpToolkit.GetString("mon", "Content");
                    }
                    if (item == DayOfWeek.Tuesday)
                    {
                       x= MultilingualHelpToolkit.GetString("tue", "Content");
                    }
                    if (item == DayOfWeek.Wednesday)
                    {
                       x= MultilingualHelpToolkit.GetString("wed", "Content");
                    }
                    if (item == DayOfWeek.Thursday)
                    {
                      x=  MultilingualHelpToolkit.GetString("thu", "Content");
                    }
                    Days += x+"  ";
                }
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
