using Core.Domain;
using Core.Todos.Tasks;
using Denna.Views;
using Microsoft.AppCenter.Analytics;
using Realms;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class TaskList : UserControl
    {
        private TodoService _service;
        public TaskList()
        {
            InitializeComponent();
            _service = new TodoService();
        }

        public IRealmCollection<Todo> TaskLists
        {
            get => (IRealmCollection<Todo>)GetValue(TaskListsProperty);
            set => SetValue(TaskListsProperty, value);
        }

        // Using a DependencyProperty as the backing store for TaskLists.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListsProperty =
            DependencyProperty.Register("TaskLists", typeof(IRealmCollection<Todo>), typeof(TaskList), new PropertyMetadata(null));

        private async void Edit_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Action From Context Menu");
            Button btn = sender as Button;
            Todo todo = btn.Tag as Todo;
            switch (btn.Name)
            {
                case "delete":
                    MessageDialog msg = new MessageDialog("Delete? Seriously?");
                    msg.Commands.Add(new UICommand("Yes", delegate
                    {
                        _service.Delete(todo);
                    }));
                    msg.Commands.Add(new UICommand("No", delegate { }));
                    await msg.ShowAsync();

                    break;
                case "edit":
                    PageMaster.current.TimeLine.Navigate(typeof(Views.SubMaster.Add.Task), todo);
                    break;
                case "Done":
                    _service.Done(todo);
                    break;
                case "Undo":
                    _service.Undone(todo);
                    break;
                case "Postpone":
                    _service.Postpone(todo);
                    break;
                default:
                    break;
            }
        }

        private void Undone_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Todo todo = btn.Tag as Todo;
            _service.Undone(todo);
        }

        private void Done_click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Todo todo = btn.Tag as Todo;
            _service.Done(todo);
        }

        private void PostponeClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Todo todo = btn.Tag as Todo;
            _service.Postpone(todo);
        }

        private void SwipeListItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}