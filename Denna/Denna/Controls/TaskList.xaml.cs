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




        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var todo = (sender as Button).Tag as Todo;
            TodoService.Delete(todo);
        }




    }
}
