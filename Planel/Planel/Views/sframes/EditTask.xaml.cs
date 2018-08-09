using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Planel.Views.sframes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTask : Page
    {
        static Core.Models.todo todo = new Core.Models.todo();
        public EditTask()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetUpPageAnimation();
            base.OnNavigatedTo(e);
            if (e.Parameter is Core.Models.todo)
            {
                todo = (Core.Models.todo)e.Parameter;
                title.Text = todo.title;
                describe.Text = todo.detail;
                var offset = new DateTimeOffset(todo.time);
                datepic.Date = offset;
                var span = new TimeSpan(todo.time.Hour, todo.time.Minute, todo.time.Second);
                timepic.Time = span;
                if (todo.notify == 0)
                {
                    rbs.IsChecked = true;
                }
                else if (todo.notify == 1)
                {
                    rbn.IsChecked = true;
                }
                else
                {
                    rba.IsChecked = true;
                }
            }
        }

        void SetUpPageAnimation()
        {
            var collection = new TransitionCollection();

            var themeR = new EdgeUIThemeTransition();

            themeR.Edge = EdgeTransitionLocation.Bottom;

            collection.Add(themeR);
            Transitions = collection;
        }

        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (title.Text != "")
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();

                // add to database
                var todate = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds);
                byte notifymode = 0;
                if (rbs.IsChecked == true)
                    notifymode = 0;

                if (rbn.IsChecked == true)
                    notifymode = 1;

                if (rba.IsChecked == true)
                    notifymode = 2;
                var item = new Core.Models.todo() { detail = describe.Text, title = title.Text, time = todate, notify = notifymode, Id = todo.Id };

                await Core.Models.Localdb.UpdateTask(item);
                Classes.worker.refresher("Add");
            }
            else
            {
                var messageDialog = new MessageDialog("Fill the title at least :)");
                messageDialog.ShowAsync();
            }
        }
    }
}