using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Core.Service.Notifications
{
    public static class BackgroundHelper
    {
        public static async Task RegisterBackgroundServices()
        {
            var req = await BackgroundExecutionManager.RequestAccessAsync();
            if (req != BackgroundAccessStatus.DeniedByUser && req != BackgroundAccessStatus.DeniedBySystemPolicy)
            {
                if (!BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "DennaLiveTile").Any())
                {
                    var LiveTileTask = new BackgroundTaskBuilder
                    {
                        Name = "DennaLiveTile",
                        TaskEntryPoint = "Denna.Services.Background.LiveTile",
                        CancelOnConditionLoss = false
                    };
                    LiveTileTask.SetTrigger(new TimeTrigger(15, false));
                    var LiveTileTaskRegistration = LiveTileTask.Register();
                }
                if (!BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "DennaQuickAction").Any())
                {
                    var QuickActionTask = new BackgroundTaskBuilder()
                    {
                        Name = "DennaQuickAction",
                        TaskEntryPoint = "Denna.Services.Background.QuickAction",
                        CancelOnConditionLoss = false
                    };

                    QuickActionTask.SetTrigger(new ToastNotificationActionTrigger());
                    var QuickActionTaskRegistration = QuickActionTask.Register();
                }


            }
        }
        public static void DeleteBackgroundServices()
        {
            var list = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "DennaLiveTile" || x.Value.Name == "DennaQuickAction");
            foreach (var item in list)
                item.Value.Unregister(true);
        }


    }
}
