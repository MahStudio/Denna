using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Core.Domain;
using Realms;
using Core.Todos.Tasks;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class TaskList : UserControl
    {
        public TaskList()
        {
            this.InitializeComponent();

        }




        public IRealmCollection<Todo> TaskLists
        {
            get { return (IRealmCollection<Todo>)GetValue(TaskListsProperty); }
            set
            {
                SetValue(TaskListsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for TaskLists.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListsProperty =
            DependencyProperty.Register("TaskLists", typeof(IRealmCollection<Todo>), typeof(TaskList), new PropertyMetadata(null));



        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            switch (btn.Name)
            {
                case "delete":
                    TodoService.Delete(todo);
                    break;
                case "edit":
                    //Edit page navigation
                    break;
                case "Done":
                    TodoService.Done(todo);
                    break;
                case "Undo":
                    TodoService.Undone(todo);
                    break;
                case "Postpone":
                    TodoService.Postpone(todo);
                    break;
                case "delete1":
                    TodoService.Delete(todo);
                    break;
                case "edit1":
                    //Edit page navigation
                    break;
                case "Done1":
                    TodoService.Done(todo);
                    break;
                case "Undo1":
                    TodoService.Undone(todo);
                    break;
                case "Postpone1":
                    TodoService.Postpone(todo);
                    break;
                default:
                    break;
            }

        }

        private void Undone_click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            TodoService.Undone(todo);
        }
        private void Done_click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            TodoService.Done(todo);
        }
        private void PostponeClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            TodoService.Postpone(todo);
        }

        private void SwipeListItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}
