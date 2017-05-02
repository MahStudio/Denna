using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views.SubMaster
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Calendar : Page
    {
        public Calendar()
        {
            this.InitializeComponent();
        }
        private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            if (args.AddedDates != null)
            {
                foreach (var item in args.AddedDates)
                {
                    var selected = FindElementInVisualTree<CalendarViewDayItem>(sender, item);
                }
            }
            if (args.RemovedDates != null)
            {
                foreach (var item in args.RemovedDates)
                {

                }
            }
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
                    if ((child as CalendarViewDayItem).Date == selectedDate.DateTime)
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Hover", true);
                    }
                    else if ((child as CalendarViewDayItem).Date.Date == DateTime.Today)
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Normal", true);
                        //styles for today's date
                    }
                    else
                    {
                        VisualStateManager.GoToState((child as CalendarViewDayItem), "Normal", true);
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
