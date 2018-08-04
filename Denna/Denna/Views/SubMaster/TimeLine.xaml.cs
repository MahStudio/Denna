using Core.Todos.Tasks;
using Denna.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeLine : Page
    {
        List<string> countries = new List<string>();
        public TimeLineViewModel VM { get; set; }
        public TimeLine()
        {

            this.InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                VM = DataContext as TimeLineViewModel;
            };
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (args.CheckCurrent())
                {
                    var search_term = txtAutoComplete.Text;
                    if (String.IsNullOrEmpty(search_term))
                    {
                        SearchView.Visibility = Visibility.Collapsed;
                        RegularView.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        SearchView.Visibility = Visibility.Visible;
                        RegularView.Visibility = Visibility.Collapsed;
                        VM.SearchResults = TodoService.FullTextSearch(search_term);
                    }

                }
            }
        }

        private void AppBarButtonUTest_Click(object sender, RoutedEventArgs e)
        {

            PageMaster.current.NavigateToUnitTests();
        }
        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            PageMaster.current.NavigateToSettings();
        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var search_term = args.QueryText;
                VM.SearchResults = TodoService.FullTextSearch(search_term);
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add.Hobby));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Add.Task));
        }
    }
}
