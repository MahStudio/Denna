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
    public class TaskListToDoneConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var items = value as IRealmCollection<Todo>;
            return items.Where(x => x.Status == 0).Count();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
