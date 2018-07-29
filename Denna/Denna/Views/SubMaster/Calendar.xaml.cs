using Core.Todos.Tasks;
using Denna.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class Calendar : Page
    {
        public CalendarViewModel ViewModel { get; set; }
        public bool LoadTime = false;
        public Calendar()
        {
            this.InitializeComponent();
            ViewModel = new CalendarViewModel();
            this.DataContext = ViewModel;
            ViewModel.TodayList = TodoService.GetTodoListForDate(DateTime.Today);


        }
        private async void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

            var a = args.AddedDates.FirstOrDefault();
            if (a.Year < 2000)
                return;
            var b = TodoService.GetTodoListForDate(a);
            ViewModel.TodayList = b;
        }
    }
}
