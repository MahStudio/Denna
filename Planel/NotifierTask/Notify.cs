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
                Core.Classes.LiveTile.livetile();
                Core.Classes.LiveTile.updatebadge();
            }
            catch { }
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
