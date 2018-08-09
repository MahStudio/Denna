using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Denna.Converters
{
    class InputScopeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var scope = new InputScope();
            var scopeName = new InputScopeName();

            scopeName.NameValue = (InputScopeNameValue)value;
            scope.Names.Add(scopeName);
            return scope;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}