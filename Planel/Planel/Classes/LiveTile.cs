using Planel.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Planel.Classes
{
    static class LiveTile
    {
        
        public static async Task livetile()
        {
           ObservableCollection<Models.todo> todolist = new ObservableCollection<todo>();
            ObservableCollection<Models.todo> result = new ObservableCollection<todo>();
            DateTime now = DateTime.Now;
            todolist= Models.Localdb.Getfordoday(now);
var tileXml =
   TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);

        var tileAttributes = tileXml.GetElementsByTagName("text");
        
            foreach (var item in todolist)
            {
                if (item.isdone == false)
                    result.Add(item);

            }
            
          if (result.Count == 0)
            {
                tileAttributes[0].AppendChild(tileXml.CreateTextNode("nothing todo today"));
            }
            else
            {
                int i = 0;
                foreach (var item in todolist)
                {
                    tileAttributes[i].AppendChild(tileXml.CreateTextNode(item.title));
                    i++;
                }
            }

            var tileNotification = new TileNotification(tileXml);
        TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            //Wide310x150Logo
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
