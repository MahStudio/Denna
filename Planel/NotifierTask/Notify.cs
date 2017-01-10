using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace NotifierTask
{
    public sealed class Notify : IBackgroundTask
    {
        BackgroundTaskDeferral _deferal;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;
            NotifyCheck();
        }
        void SendToastNotification(string Content, string Parameters, bool IsDurationLong = false)
        {
            var toastTemplate = ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(Content));
            var toastNode = toastXml.SelectSingleNode("/toast");
            if (IsDurationLong)
                ((Windows.Data.Xml.Dom.XmlElement)toastNode).SetAttribute("duration", "long");
            ((Windows.Data.Xml.Dom.XmlElement)toastNode).SetAttribute("launch", Parameters);
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        async void NotifyCheck()
        {
            var Notifications = new List<todo>();
            var list = Localdb.Getfordoday(DateTime.Now);
            foreach (var item in list)
            {
                if (item.notify == 1)
                {
                    var sub = item.time.Subtract(DateTime.Now);
                    if (sub.TotalMinutes >= 0 && sub.TotalMinutes <= 15)
                    {
                        Notifications.Add(item);
                    }
                }
            }
            foreach (var item in Notifications)
            {
                SendToastNotification(item.title + " : " + item.detail, item.Id.ToString(), true);
            }
        }

        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            _deferal.Complete();
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            _deferal.Complete();
        }
    }
}
