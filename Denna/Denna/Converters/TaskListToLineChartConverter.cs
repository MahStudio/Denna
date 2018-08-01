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
    public class TaskListToLineChartConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var items = value as IRealmCollection<Todo>;
            var chartItems = new ObservableCollection<NameValueItem>();
            foreach (var item in items)
            {
                chartItems.Add(new NameValueItem()
                {
                    Name = item.Id,
                    Value = _random.Next(10, 100)
                });
            }
    }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
