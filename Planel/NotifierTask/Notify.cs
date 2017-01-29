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
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;
            
            try { 
            livetile();

            updatebadge();
            }
            catch { }
        }

        

       
        public static async void livetile()
        {
            ObservableCollection<todo> todolist = new ObservableCollection<todo>();
            int counter = Localdb.counter();
            string result = " Dont forget to :  ";
            if (counter == 0)
            {
                result = "Nothing to do today :) add something todo. ";

            }
            else
            {

                DateTime now = DateTime.Now;
                todolist = Localdb.Getfordoday(now);
                foreach (var item in todolist)
                {
                    if (item.isdone != 2)
                        result += "," + item.title + "  ";

                }
            }
            result.Replace("\n", Environment.NewLine);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(await FileIO.ReadTextAsync(await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///LiveTile.xml"))));
            //Set Medium tile
            // Fuck me baby
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile = await storageFolder.GetFileAsync("avatar.jpg");
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumImageSource", sampleFile.Path));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumtext", "Dear " + ApplicationData.Current.LocalSettings.Values["Username"].ToString()));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileMediumSubText", "Let's Do!"));
            //Set Wide Tile 
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideImageSource", sampleFile.Path));
            xmlDoc.LoadXml(xmlDoc.GetXml().Replace("TileWideText", result));



            var tup = TileUpdateManager.CreateTileUpdaterForApplication();
            tup.Update(new TileNotification(xmlDoc));






        }
        public static async void updatebadge()
        {
            var type = BadgeTemplateType.BadgeNumber;
            var xml = BadgeUpdateManager.GetTemplateContent(type);

            var elements = xml.GetElementsByTagName("badge");
            var element = elements[0] as Windows.Data.Xml.Dom.XmlElement;
            int val = Localdb.counter();
            element.SetAttribute("value", val.ToString());

            var updator = BadgeUpdateManager.CreateBadgeUpdaterForApplication();
            var notification = new BadgeNotification(xml);
            updator.Update(notification);
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
