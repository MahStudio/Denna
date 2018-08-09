using Core.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Core.Classes
{
    public static class LiveTile
    {
        const string TRANSLATOR_GROUP = "Translator";
        public static async void GenerateToast()
        {
            var toastFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(
                   new Uri("ms-appx:///Xtoast.xml"));
            var xmlString = await FileIO.ReadTextAsync(toastFile);
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(xmlString);
            var storageFolder22 = ApplicationData.Current.LocalFolder;
            var sampleFile22 = await storageFolder22.GetFileAsync("avatar.jpg");
            doc.LoadXml(doc.GetXml().Replace("Prophyle", sampleFile22.Path));
            var toast = new ToastNotification(doc)
            {
                Group = TRANSLATOR_GROUP,
                SuppressPopup = true
            };
            toast.Dismissed += Toast_Dismissed;
            toast.Activated += ToastOnActivated;
            toast.Failed += Toast_Failed;

            var history = ToastNotificationManager.History.GetHistory();
            if (!history.Any(t => t.Group.Equals(TRANSLATOR_GROUP)))
                ToastNotificationManager.CreateToastNotifier().Show(toast);
            await Task.Delay(1000);
        }

        static async void Toast_Failed(ToastNotification sender, ToastFailedEventArgs args)
        {
            GenerateToast();
        }

        static async void ToastOnActivated(ToastNotification sender, object args)
        {
            GenerateToast();
        }

        static async void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args)
        {
            GenerateToast();
        }

        public static async Task livetile()
        {
            ObservableCollection<Models.todo> todolist = new ObservableCollection<todo>();
            var counter = Localdb.counter();
            var result = " Dont forget to :  ";
            if (counter == 0)
            {
                result = "Nothing to do today :) add something todo. ";
            }
            else
            {
                var now = DateTime.Now;
                todolist = Models.Localdb.getall(now);
                foreach (var item in todolist)
                {
                    if (item.isdone != 2)
                        result += " " + item.title + ",";
                }
            }

            result.Replace("\n", Environment.NewLine);

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///LiveTile.xml"))));
            // Set Medium tile
            var storageFolder = ApplicationData.Current.LocalFolder;
            var sampleFile = await storageFolder.GetFileAsync("avatar.jpg");
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumImageSource", sampleFile.Path));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumtext", "Dear " + ApplicationData.Current.LocalSettings.Values["Username"].ToString()));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumSubText", "Let's Do!"));
            // Set Wide Tile
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideImageSource", sampleFile.Path));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideText", result));
            // Set LockScreen Detail
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("DetailLC", ApplicationData.Current.LocalSettings.Values["Username"].ToString() + "," + result));

            var tup = TileUpdateManager.CreateTileUpdaterForApplication();
            tup.Update(new TileNotification(xmlDoc));
        }

        public static async Task updatebadge()
        {
            var type = BadgeTemplateType.BadgeNumber;
            var xml = BadgeUpdateManager.GetTemplateContent(type);

            var elements = xml.GetElementsByTagName("badge");
            var element = elements[0] as Windows.Data.Xml.Dom.XmlElement;
            var val = Models.Localdb.counter();
            element.SetAttribute("value", val.ToString());

            var updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            var notification = new BadgeNotification(xml);
            updator.Update(notification);
        }
    }
}