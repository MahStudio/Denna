using Core.Domain;
using Core.Todos.Tasks;
using PubSub;
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
        TodoService _service;
        bool editmode = false;
        Todo editing = null;
        public Task()
        {
            InitializeComponent();
            _service = new TodoService();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            if (e.Parameter is Todo)
            {
                editing = e.Parameter as Todo;
                Title.Text = editing.Subject;
                Details.Text = editing.Detail;
                datepic.Date = editing.StartTime;
                timepic.Time = editing.StartTime.ToLocalTime().TimeOfDay;
                if (editing.Notify == 0)
                    rbs.IsChecked = true;
                else if (editing.Notify == 1)
                    rbn.IsChecked = true;
                else
                    rba.IsChecked = true;

                editmode = true;
            }
            if (editmode)
                this.Publish(new Classes.Header("Edit"));
            else
                this.Publish(new Classes.Header("Add"));
            base.OnNavigatedTo(e);
        }


        void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
            this.Publish(new Classes.Header("Timeline"));
        }

        void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var start = new DateTime(datepic.Date.Year, datepic.Date.Month, datepic.Date.Day, timepic.Time.Hours, timepic.Time.Minutes, timepic.Time.Seconds);

            if (Title.Text == "")
            {
                ErrorText.Text = "Please set a title for this alarm.";
                return;
            }


            if (start < DateTime.Now)
            {
                ErrorText.Text = "Can't set a time in past.";
                return;
            }

         
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
            if (editmode)
            {
                todo.Id = editing.Id;
                _service.Edit(editing, todo);
            }
            else
            {

                _service.AddTodo(todo);
            }
            Frame.GoBack();
            this.Publish(new Classes.Header("Timeline"));
        }
    }
}
