using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace NotifierTask
{
    public sealed class Notify : IBackgroundTask
    {
        BackgroundTaskDeferral _deferal;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;
            


            try
            {
                Core.Classes.LiveTile.livetile();
                Core.Classes.LiveTile.updatebadge();
                if (ApplicationData.Current.LocalSettings.Values["Showtoast"] != null)
                {
                    if ((bool)ApplicationData.Current.LocalSettings.Values["Showtoast"] == true)
                    {
                        await GenerateToast();
                    }
                }
            }
            catch { }
            
        }
        const string TranslatorGroup = "Translator";
        private async Task GenerateToast()
        {
            var toastFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                   new Uri("ms-appx:///Xtoast.xml"));
            var xmlString = await FileIO.ReadTextAsync(toastFile);
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(xmlString);
                   StorageFolder storageFolder22 = ApplicationData.Current.LocalFolder;
                   StorageFile sampleFile22 = await storageFolder22.GetFileAsync("avatar.jpg");
                    doc.LoadXml(doc.GetXml().Replace("Prophyle", sampleFile22.Path));
            var toast = new ToastNotification(doc)
            {
                Group = TranslatorGroup,
                SuppressPopup = true
            };
            toast.Dismissed += Toast_Dismissed;
            toast.Activated += ToastOnActivated;
            toast.Failed += Toast_Failed;

            var history = ToastNotificationManager.History.GetHistory();
            if (!history.Any(t => t.Group.Equals(TranslatorGroup)))
                ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

        private async void Toast_Failed(ToastNotification sender, ToastFailedEventArgs args)
        {
            await GenerateToast();
        }

        private async void ToastOnActivated(ToastNotification sender, object args)
        {
            await GenerateToast();
        }

        private async void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            await GenerateToast();
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
