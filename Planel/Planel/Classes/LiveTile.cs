using Microsoft.Toolkit.Uwp.Notifications;
using Planel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace Planel.Classes
{
    static class LiveTile
    {
        
        public static async Task livetile()
        {


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
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideText", "4Khoune , j k kj , jk kj kj , jkjnkjnk, jknjmn,"));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideSubText", ""));
            

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
