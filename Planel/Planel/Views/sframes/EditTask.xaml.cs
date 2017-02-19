using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
       static  Core.Models.todo todo = new Core.Models.todo();
        public EditTask()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SetUpPageAnimation();
            base.OnNavigatedTo(e);
            if (e.Parameter is Core.Models.todo)
            {
                
                todo = (Core.Models.todo) e.Parameter;
                title.Text = todo.title;
                describe.Text = todo.detail;
                DateTimeOffset offset = new DateTimeOffset(todo.time);
                datepic.Date = offset;
                TimeSpan Span = new TimeSpan(todo.time.Hour, todo.time.Minute, todo.time.Second);
                timepic.Time = Span;
                if (todo.notify == 0)
                {
                    rbs.IsChecked = true;
                }
                else if(todo.notify ==1)
                {
                    rbn.IsChecked = true;
                }
                else
                {
                    rba.IsChecked = true;
                }
            }
        }
        private void SetUpPageAnimation()
        {
            TransitionCollection collection = new TransitionCollection();


            var themeR = new EdgeUIThemeTransition();

            themeR.Edge = EdgeTransitionLocation.Bottom;


            collection.Add(themeR);
            this.Transitions = collection;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
                Frame.GoBack();


        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (title.Text != "")
            {
                if (Frame.CanGoBack)
                    Frame.GoBack();

                //add to database
                DateTime todate = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds) ;
                byte notifymode = 0;
                if (rbs.IsChecked == true)
                    notifymode = 0;

                if (rbn.IsChecked == true)
                    notifymode = 1;

                if (rba.IsChecked == true)
                    notifymode = 2;
                Core.Models.todo item = new Core.Models.todo() { detail = describe.Text, title = title.Text, time = todate, notify = notifymode, Id=todo.Id};


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
