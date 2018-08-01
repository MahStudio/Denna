using Core.Domain;
using Denna.Classes;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    public class TaskListToMonthlyLineChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var items = value as IRealmCollection<Todo>;
            var chartItems = new ObservableCollection<NameValueItem>();
            var max = items.Max(x => x.StartTime);
            var min = max.AddMonths(-1);
            for (int i = 0; i < (max - min).TotalDays - 1; i++)
            {
                var thDate = max.AddDays(-i);
                var start = new DateTime(thDate.Year, thDate.Month, thDate.Day, 0, 0, 0);
                var end = new DateTime(thDate.Year, thDate.Month, thDate.Day, 23, 59, 59);
                var itemsOfTheDay = items.Where(y => y.StartTime > start && y.StartTime < end);
                if (itemsOfTheDay.Any())
                {
                    int done = itemsOfTheDay.Where(z => z.Status == 0).Count();
                    chartItems.Add(new NameValueItem()
                    {
                        Name = "test" + i,
                        Value = (done / itemsOfTheDay.Count()) * 100
                    });
                }
                else
                {
                    chartItems.Add(new NameValueItem()
                    {
                        Name = "test" + i,
                        Value = 0
                    });
                }
            }

            return chartItems;
            //chartItems.Add(new NameValueItem()
            //{
            //    Name = item.Id,
            //    Value = _random.Next(10, 100)
            //});
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
