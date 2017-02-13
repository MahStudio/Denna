
using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Core.Classes
{
    public static class LiveTile
    {
        
        public static async Task livetile()
        {
           
            //    if ((bool)ApplicationData.Current.LocalSettings.Values["Showtoast"] == true)
            //    {
            //        XmlDocument doc = new XmlDocument();
            //        doc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
            //            new Uri("ms-appx:///Xtoast.xml"))));
            //        StorageFolder storageFolder22 = ApplicationData.Current.LocalFolder;
            //        StorageFile sampleFile22 = await storageFolder22.GetFileAsync("avatar.jpg");
            //        doc.LoadXml(doc.GetXml().Replace("Prophyle", sampleFile22.Path));
            //        var toast = new ToastNotification(doc);

            //        ToastNotificationManager.History.Remove("Qaction");
            //        ToastNotificationManager.History.Remove("Qaction", "Quick Denna");
            //        toast.Tag = "Qaction";
            //        toast.Group = "Quick Denna";
            //        toast.SuppressPopup = true;
            //        ToastNotificationManager.CreateToastNotifier().Show(toast);
            //    }
            //}
            ObservableCollection<Models.todo> todolist = new ObservableCollection<todo>();
            int counter = Localdb.counter();
            string result = " Dont forget to :  ";
            if (counter == 0)
            {
                result = "Nothing to do today :) add something todo. ";

            }
            else { 
            
            DateTime now = DateTime.Now;
            todolist = Models.Localdb.getall(now);
            foreach (var item in todolist)
            {
                if (item.isdone != 2)
                    result += " "+item.title + "," ;

            }
            }
            result.Replace("\n", Environment.NewLine);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///LiveTile.xml"))));
            //Set Medium tile
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("avatar.jpg");
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumImageSource", sampleFile.Path ));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumtext", "Dear " + ApplicationData.Current.LocalSettings.Values["Username"].ToString()));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumSubText", "Let's Do!"));
            //Set Wide Tile 
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideImageSource", sampleFile.Path));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideText",  result));
            //Set LockScreen Detail
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("DetailLC", ApplicationData.Current.LocalSettings.Values["Username"].ToString()+","+ result));
            


            var tup = TileUpdateManager.CreateTileUpdaterForApplication();
            tup.Update(new TileNotification(xmlDoc));
            





        }
        public static async Task updatebadge()
        {

            var type = BadgeTemplateType.BadgeNumber;
            var xml = BadgeUpdateManager.GetTemplateContent(type);

            var elements = xml.GetElementsByTagName("badge");
            var element = elements[0] as Windows.Data.Xml.Dom.XmlElement;
            int val = Models.Localdb.counter();
            element.SetAttribute("value", val.ToString());

            var updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            var notification = new BadgeNotification(xml);
            updator.Update(notification);
        }
        



    }
}
