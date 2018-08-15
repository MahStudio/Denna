using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Core.Service.Notifications
{
    public static class NotificationService
    {
        public static async void CreateAlarm(this Todo item)
        {
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri("ms-appx:///XMLs/AlarmToast.xml"))));
            doc.LoadXml(doc.GetXml().Replace("Header", item.Subject));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.Detail));
            DateTimeOffset offset = item.StartTime;
            var toast = new ScheduledToastNotification(doc, offset);
            toast.Id = item.Id.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
        public static void CreateNotification(this Todo item)
        {
            var xml = @"<toast>
            <visual>
            <binding template=""ToastGeneric"">
                <text>Header</text>
                <text>Detail</text>
                <text placement='attribution'>Right now</text>
            </binding>
            </visual>
        </toast>";

            var doc = new Windows.Data.Xml.Dom.XmlDocument();

            doc.LoadXml(xml);
            doc.LoadXml(doc.GetXml().Replace("Header", item.Subject));
            doc.LoadXml(doc.GetXml().Replace("Detail", item.Detail));
            var offset = item.StartTime.ToLocalTime();
            var toast = new ScheduledToastNotification(doc, offset);
            toast.Id = item.Id;

            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
        public static void UpdateNotification(this Todo item)
        {
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            foreach (var scheduledToastNotification in toastnotifier.GetScheduledToastNotifications())
            {
                if (scheduledToastNotification.Id == item.Id)
                {
                    toastnotifier.RemoveFromSchedule(scheduledToastNotification);
                }
            }

            if (item.Notify == 1)
                item.CreateNotification();
            else if (item.Notify == 2)
                item.CreateAlarm();
        }
        public static void DeleteNotification(this Todo item)
        {
            var toastnotifier = ToastNotificationManager.CreateToastNotifier();

            foreach (var scheduledToastNotification in toastnotifier.GetScheduledToastNotifications())
            {
                if (scheduledToastNotification.Id == item.Id)
                {
                    toastnotifier.RemoveFromSchedule(scheduledToastNotification);
                }
            }
        }
    }
}
