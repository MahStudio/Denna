using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Denna.Converters
{
    class InputScopeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            InputScope scope = new InputScope();
            InputScopeName scopeName = new InputScopeName();
            
            scopeName.NameValue = (InputScopeNameValue) value;
            scope.Names.Add(scopeName);
            return scope;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
