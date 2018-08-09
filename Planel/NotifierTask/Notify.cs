using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace NotifierTask
{
    public sealed class Notify : IBackgroundTask
    {
        BackgroundTaskDeferral deferal;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            deferal = taskInstance.GetDeferral();
            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;

            try
            {
                Core.Classes.LiveTile.livetile();
                Core.Classes.LiveTile.updatebadge();
                if (ApplicationData.Current.LocalSettings.Values["Showtoast"] != null)
                {
                    if ((bool)ApplicationData.Current.LocalSettings.Values["Showtoast"] == true)
                    {
                        Core.Classes.LiveTile.GenerateToast();
                    }
                }
            }
            catch { }
        }

        void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            deferal.Complete();
        }

        void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            deferal.Complete();
        }
    }
}