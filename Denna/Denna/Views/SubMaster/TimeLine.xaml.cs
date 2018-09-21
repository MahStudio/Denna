using Core.Todos.Tasks;
using Denna.ViewModels;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TimeLine : Page
    {
        private List<string> countries = new List<string>();
        public TimeLineViewModel VM { get; set; }
        public static TimeLine current;
        private TodoService _service;
        public TimeLine()
        {
            InitializeComponent();
            DataContextChanged += (s, e) =>
            {
                VM = DataContext as TimeLineViewModel;
                _service = new TodoService();
            };
            current = this;
        }
        public void DoOutsiderSearch(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                SearchView.Visibility = Visibility.Collapsed;
                RegularView.Visibility = Visibility.Visible;
            }
            else
            {
                SearchView.Visibility = Visibility.Visible;
                RegularView.Visibility = Visibility.Collapsed;
                VM.SearchResults = _service.FullTextSearch(term);
                txtAutoComplete.Text = term;
            }
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
                    string search_term = txtAutoComplete.Text;
                    if (string.IsNullOrEmpty(search_term))
                    {
                        SearchView.Visibility = Visibility.Collapsed;
                        RegularView.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        SearchView.Visibility = Visibility.Visible;
                        RegularView.Visibility = Visibility.Collapsed;
                        VM.SearchResults = _service.FullTextSearch(search_term);
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
                string search_term = args.QueryText;
                VM.SearchResults = _service.FullTextSearch(search_term);
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