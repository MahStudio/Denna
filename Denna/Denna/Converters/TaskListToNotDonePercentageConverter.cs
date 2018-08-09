using Core.Domain;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    public class TaskListToNotDonePercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var items = value as IRealmCollection<Todo>;
            var undone = items.Where(x => x.Status != 0).Count();
            if (items.Count == 0 || undone == 0)
                return 0.0;
            return Math.Round(undone / (double)items.Count, 2) * 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
