using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace Toaster
{
    public sealed class Toaster : IBackgroundTask
    {
        BackgroundTaskDeferral _deferal;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;
            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;

            if (details != null)
            {
                try
                {
                    string arguments = details.Argument;
                    var userInput = details.UserInput;
                    string title = (string)userInput["title"];
                    string detail = (string)userInput["detail"];
                    var notify = (byte)int.Parse(userInput["notification"].ToString());
                    var Time = int.Parse(userInput["snoozeTime"].ToString());
                    DateTime now = DateTime.Now;
                    if (Time == 15)
                    {
                        now.AddMinutes(15);
                    }
                    else if (Time == 60)
                    {
                        now.AddHours(1);
                    }
                    else if(Time==140)
                    {
                        now.AddHours(4);
                    }
                    else if (Time == 160)
                    {
                        now.AddHours(8);

                    }
                    else if (Time == 190)
                    {
                        now.AddDays(1);
                    }
                    Core.Models.todo task = new Core.Models.todo() {detail=detail , title=title,notify=notify ,time=now.ToLocalTime()};
                    Core.Models.Localdb.Addtodo(task);
                }
                catch (Exception ex) { }
                try
                {
                    sthr();
                }
                catch
                {

                }
            }
            
        }
        private async void sthr()
        {
            await Core.Classes.LiveTile.livetile();
           await Core.Classes.LiveTile.GenerateToast();
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
