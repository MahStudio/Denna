using Core.Domain;
using Core.Service.Notifications;
using Core.Todos.Tasks;
using Core.Utils;
using System;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace Denna.Services.Background
{
    public sealed class QuickAction : IBackgroundTask
    {
        BackgroundTaskDeferral _deferal;
        TodoService _service;
        BackgroundService _bgService;
        public QuickAction()
        {
            _service = new TodoService();
            _bgService = new BackgroundService(_service);
        }
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            System.Diagnostics.Debug.WriteLine("Hello From Quick actions bg");
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
                    DateTimeOffset now = DateTimeOffset.Now;

                    if (Time == 15)
                    {
                        DateTime nulll = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        now = now.AddMinutes(15);
                    }
                    else if (Time == 60)
                    {
                        DateTime nulll = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        now.AddHours(1);
                    }
                    else if (Time == 140)
                    {
                        DateTime nulll = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        now.AddHours(4);
                    }
                    else if (Time == 160)
                    {
                        DateTime nulll = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        now.AddHours(8);

                    }
                    else if (Time == 190)
                    {
                        DateTime nulll = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
                        now.AddDays(1);
                    }
                    DateTime pocker = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);

                    var todo = new Todo()
                    {
                        Subject = title,
                        Detail = detail,
                        StartTime = pocker,
                        Notify = notify,
                        Status = 2
                    };

                    _service.AddTodo(todo);
                }
                catch (Exception ex) { }
                try
                {
                    Update();
                }
                catch
                {

                }
            }
            _deferal.Complete();
        }
        private void Update()
        {

            _bgService.GenerateLiveTile();
            _bgService.UpdateBadge();
            if (AppSettings.OpenGet("Showtoast") != null)
            {
                if (AppSettings.Get<bool>("Showtoast") == true)
                {
                    _bgService.GenerateQuickAction();
                }
            }
        }
        private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args) => _deferal.Complete();

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason) => _deferal.Complete();
    }
}
