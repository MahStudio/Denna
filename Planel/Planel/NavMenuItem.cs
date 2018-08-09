using System;
using Windows.UI.Xaml.Controls;

namespace Planel.Views
{
    public class NavMenuItem
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
        public char SymbolAsChar => (char)Symbol;

        public Type DestinationPage { get; set; }
        public object Arguments { get; set; }
    }
}