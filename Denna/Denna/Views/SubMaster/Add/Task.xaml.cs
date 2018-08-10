using Core.Domain;
using Core.Todos.Tasks;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster.Add
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Task : Page
    {
        public Task()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e) => base.OnNavigatedTo(e);

        void AppBarButton_Click(object sender, RoutedEventArgs e) => Frame.GoBack();

        void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var start = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds);
            int notiftStatus = 0;
            if (rbs.IsChecked == true)
                notiftStatus = 0;

            if (rbn.IsChecked == true)
                notiftStatus = 1;

            if (rba.IsChecked == true)
                notiftStatus = 2;
            var todo = new Todo()
            {
                Subject = Title.Text,
                Detail = Details.Text,
                StartTime = start,
                Notify = notiftStatus,
                Status = 2
            };
            TodoService.AddTodo(todo);
            Frame.GoBack();
        }
    }
}
