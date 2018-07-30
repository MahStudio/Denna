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
        public DateTimeOffset SelectedDate { get => MyCalendarView.SelectedDates.First(); }
        public bool LoadTime = false;
        public Calendar()
        {
            this.InitializeComponent();


            DataContextChanged += (s, e) =>
            {
                ViewModel = DataContext as CalendarViewModel;
            };


        }

        private void MyCalendarView_Loaded(object sender, RoutedEventArgs e)
        {
            CalendarView loadedCalendarView = sender as CalendarView;
            if (loadedCalendarView != null)
            {
                SelectCurrentDate(loadedCalendarView);

            }
        }

        private void SelectCurrentDate(CalendarView MyCalendarView)
        {
            FindCurrentDateElementInVisualTree<CalendarViewDayItem>(MyCalendarView);
        }

        private async void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

            Doer(sender, args);

        }
        async void Doer(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {

            if (args.AddedDates != null)
            {

                foreach (var item in args.AddedDates)
                {

                    var selected = FindElementInVisualTree<CalendarViewDayItem>(MyCalendarView, item);
                }



            }

            if (args.RemovedDates != null)
            {
                foreach (var item in args.RemovedDates)
                {

                }
            }


        }

        public static T FindCurrentDateElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0) return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is CalendarViewDayItem)
                {
                    if ((child as CalendarViewDayItem).Date.UtcDateTime.Date == DateTimeOffset.UtcNow.Date)
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Selected", true);
                    }
                }
                else
                {
                    var result = FindCurrentDateElementInVisualTree<T>(child);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }


        public static T FindElementInVisualTree<T>(DependencyObject parentElement, DateTimeOffset selectedDate) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0) return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is CalendarViewDayItem)
                {
                    if ((child as CalendarViewDayItem).Date.Date == selectedDate.Date)
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Selected", true);
                    }

                    else
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Unselected", true);
                    }
                }
                else
                {
                    var result = FindElementInVisualTree<T>(child, selectedDate);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }


    }
}
