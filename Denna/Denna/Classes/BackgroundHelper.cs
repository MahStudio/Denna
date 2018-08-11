using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace Denna.Classes
{
    public static class BackgroundHelper
    {
        public static async Task RegisterBackgroundServices()
        {
            var req = await BackgroundExecutionManager.RequestAccessAsync();
            if (req != BackgroundAccessStatus.DeniedByUser && req != BackgroundAccessStatus.DeniedBySystemPolicy)
            {
                var list = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "LiveTile");
                foreach (var item in list)
                    item.Value.Unregister(false);


                var taskBuilder = new BackgroundTaskBuilder
                {
                    Name = "LiveTile",
                    TaskEntryPoint = "Denna.Services.Background.QuickAction.LiveTile",
                    CancelOnConditionLoss = false
                };
                taskBuilder.SetTrigger(new TimeTrigger(15, false));

                var list2 = BackgroundTaskRegistration.AllTasks.Where(x => x.Value.Name == "QuickAction");
                foreach (var item in list2)
                    item.Value.Unregister(false);


                var builder = new BackgroundTaskBuilder()
                {
                    Name = "QuickAction",
                    TaskEntryPoint = "Denna.Services.Background.QuickAction",
                    CancelOnConditionLoss = false
                };

                builder.SetTrigger(new ToastNotificationActionTrigger());
                var registration = builder.Register();
                var myFirstTask = taskBuilder.Register();
            }
        }
    }
}
