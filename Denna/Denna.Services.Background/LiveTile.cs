using Core.Service.Notifications;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Denna.Services.Background
{
    public sealed class LiveTile : IBackgroundTask
    {
        BackgroundTaskDeferral deferal;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;
            System.Diagnostics.Debug.WriteLine("Hello From Live bg");
            try
            {
                BackgroundService.GenerateLiveTile();
                BackgroundService.UpdateBadge();
                if (AppSettings.OpenGet("Showtoast") != null)
                {
                    if (AppSettings.Get<bool>("Showtoast") == true)
                    {
                        BackgroundService.GenerateQuickAction();
                    }
                }
            }
            catch { }
        }

        void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args) => deferal.Complete();

        void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason) => deferal.Complete();
    }
}
