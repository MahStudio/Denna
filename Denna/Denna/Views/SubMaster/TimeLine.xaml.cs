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
            countries = new List<string> { "United Kingdom", "United States", "United Arab Emrites", "ahmed", "India", "Canada" };
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
                    var results = countries.Where(i => i.StartsWith(search_term)).ToList();
                    txtAutoComplete.ItemsSource = results;
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
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            txtAutoComplete.Text = args.SelectedItem as string;

        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var search_term = args.QueryText;
                var results = countries.Where(i => i.StartsWith(search_term)).ToList();
                txtAutoComplete.ItemsSource = results;
                txtAutoComplete.IsSuggestionListOpen = true;
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
