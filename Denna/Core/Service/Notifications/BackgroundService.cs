using Core.Todos.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Core.Service.Notifications
{
    public static class BackgroundService
    {
        const string TRANSLATOR_GROUP = "Translator";

        public static async void GenerateQuickAction()
        {
            System.Diagnostics.Debug.WriteLine("Hello From Quick actions caller");
            var toastFile = await StorageFile.GetFileFromApplicationUriAsync(
                   new Uri("ms-appx:///XMls/QuickAction.xml"));
            var xmlString = await FileIO.ReadTextAsync(toastFile);
            var doc = new Windows.Data.Xml.Dom.XmlDocument();
            doc.LoadXml(xmlString);
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

        static void Toast_Failed(ToastNotification sender, ToastFailedEventArgs args) => GenerateQuickAction();

        static void ToastOnActivated(ToastNotification sender, object args) => GenerateQuickAction();

        static void Toast_Dismissed(ToastNotification sender, ToastDismissedEventArgs args) => GenerateQuickAction();

        public static async void GenerateLiveTile()
        {
            System.Diagnostics.Debug.WriteLine("Hello From live tile caller");
            var tasks = TodoService.GetMustDoList();
            if (tasks.Count == 0)
                return;
            var counter = tasks.Count;


            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///XMLs/LiveTile.xml"))));
            Random rnd = new Random();
            int r = rnd.Next(tasks.Count);
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TaskName", tasks[r].Subject));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("Detail", tasks[r].Detail));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("Time", tasks[r].StartTime.Convert()));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("Date", CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month) + "  " + DateTime.Now.Day));
            var tup = TileUpdateManager.CreateTileUpdaterForApplication();
            tup.Update(new TileNotification(xmlDoc));
        }
        static string Convert(this DateTimeOffset Value)
        {
            var Day = Value.Date;
            var x = "";
            if (Day == DateTime.Today)
                x += "Today";
            else if (Day == DateTime.Today.AddDays(1))
                x += "Tomorrow";
            else if (Day == DateTime.Today.AddDays(-1))
                x += "Yesterday";
            else
            {
                var month = Value.Month;
                var day = Value.Day;
                x += month + "/" + day;
            }

            var hour = Value.Hour.ToString();
            var min = Value.Minute.ToString();

            x += " " + hour + ":" + min;
            return x;
        }
        public static void UpdateBadge()
        {
            System.Diagnostics.Debug.WriteLine("Hello From Quick update badge caller");

            var type = BadgeTemplateType.BadgeNumber;
            var xml = BadgeUpdateManager.GetTemplateContent(type);

            var elements = xml.GetElementsByTagName("badge");
            var element = elements[0] as Windows.Data.Xml.Dom.XmlElement;
            var val = TodoService.GetMustDoList().Count;
            element.SetAttribute("value", val.ToString());

            var updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            var notification = new BadgeNotification(xml);
            updator.Update(notification);
        }

        public static void UpdateTiles()
        {
            UpdateBadge();
            GenerateLiveTile();
        }
    }
}
