using Core.Domain;
using Core.Todos.Tasks;
using Microsoft.AppCenter.Analytics;
using Realms;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Denna.Controls
{
    public sealed partial class TaskList : UserControl
    {
        TodoService _service;
        public TaskList()
        {
            InitializeComponent();
            _service = new TodoService();
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

        void Edit_Click(object sender, RoutedEventArgs e)
        {
            Analytics.TrackEvent("Action From Context Menu");
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            switch (btn.Name)
            {
                case "delete":
                    _service.Delete(todo);
                    break;
                case "edit":
                    // Edit page navigation
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

        void Undone_click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            _service.Undone(todo);
        }

        void Done_click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            _service.Done(todo);
        }

        void PostponeClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var todo = btn.Tag as Todo;
            _service.Postpone(todo);
        }

        void SwipeListItem_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}