using Core.Domain;
using Realms;
using System;
using System.Linq;
using Windows.UI.Xaml.Data;

namespace Denna.Converters
{
    public class TaskListToPostponedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var items = value as IRealmCollection<Todo>;
            return items.Where(x => x.Status == 1).Count();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
