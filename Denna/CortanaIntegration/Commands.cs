using Core.Todos.Tasks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;
using Realms;
using System.Diagnostics;

namespace Denna.Cortana
{
    public sealed class Commands : IBackgroundTask
    {
        // fuck you cortana :|
        BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            Debug.WriteLine("Fug me");
            // Take a service deferral so the service isn&#39;t terminated.
            serviceDeferral = taskInstance.GetDeferral();

            taskInstance.Canceled += TaskInstance_Canceled;
            taskInstance.Task.Completed += Task_Completed;

            var triggerDetails =
                taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (triggerDetails != null && triggerDetails.Name == "Bgcommand")
            {
                try
                {
                    voiceServiceConnection =
                        VoiceCommandServiceConnection.FromAppServiceTriggerDetails(
                            triggerDetails);

                    voiceServiceConnection.VoiceCommandCompleted +=
                        VoiceCommandCompleted;

                    var voiceCommand = await
                        voiceServiceConnection.GetVoiceCommandAsync();
                    switch (voiceCommand.CommandName)
                    {
                        case "ToDoFetcher":
                            {
                                SendCompletionMessageFortodolist();
                                break;
                            }
                        case "TodoCounter":
                            {
                                taskcounter();
                                break;
                            }
                        case "addtask":
                            {
                                LaunchAppInForeground();
                                break;
                            }

                        //   As a last resort, launch the app in the foreground.
                        default:
                            LaunchAppInForeground();
                            break;
                    }
                }
                finally
                {
                    if (serviceDeferral != null)
                    {
                        // Complete the service deferral.
                        serviceDeferral.Complete();
                    }
                }
            }
        }

        void VoiceCommandCompleted(
           VoiceCommandServiceConnection sender,
           VoiceCommandCompletedEventArgs args)
        {
            if (serviceDeferral != null)
            {
                // Insert your code here.
                // Complete the service deferral.
                serviceDeferral.Complete();
            }
        }

        void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            serviceDeferral.Complete();
        }

        void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            serviceDeferral.Complete();
        }


        async void taskcounter()
        {
            var x = TodoService.GetMustDoList().Count;
            VoiceCommandResponse response = null;
            if (x == 0)
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "No tasks on Denna";
                userMessage.SpokenMessage = "You have no tasks ! Add one";
                response =
                 VoiceCommandResponse.CreateResponse(userMessage);
            }
            else
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "You have " + x + " tasks" + " on Denna";
                userMessage.SpokenMessage = "You have " + x + " tasks";
                response =
                 VoiceCommandResponse.CreateResponse(userMessage);
            }

            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.

            // Cortana will present a “Go to app_name” link that the user 
            // can tap to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument = "agsonCortana";

            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await voiceServiceConnection.ReportSuccessAsync(response);
        }




        async void SendCompletionMessageFortodolist()
        {
            var destinationsContentTiles = new List<VoiceCommandContentTile>();
            var mycol = TodoService.GetTodayList();
            VoiceCommandResponse response = null;
            if (mycol.Count == 0)
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "Nothing in Denna";
                userMessage.SpokenMessage = "You have no tasks ! add one.";
                response = VoiceCommandResponse.CreateResponse(userMessage);
            }
            else
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "Here's your to do list on Denna";
                userMessage.SpokenMessage = "Here's your to do list";
                foreach (var item in mycol)
                {
                    var destinationTile = new VoiceCommandContentTile();
                    destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
                    destinationTile.AppLaunchArgument = "agsonCortana";
                    destinationTile.Title = item.Subject;
                    destinationTile.TextLine1 = item.Detail;
                    destinationTile.TextLine2 = item.StartTime.ToString();
                    destinationsContentTiles.Add(destinationTile);
                    response =
               VoiceCommandResponse.CreateResponse(
                   userMessage, destinationsContentTiles);
                }
            }

            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.

            // Cortana will present a “Go to app_name” link that the user 
            // can tap to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument = "agsonCortana";

            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await voiceServiceConnection.ReportSuccessAsync(response);
        }

        async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Denna";

            var response = VoiceCommandResponse.CreateResponse(userMessage);

            // When launching the app in the foreground, pass an app 
            // specific launch parameter to indicate what page to show.
            response.AppLaunchArgument = "agsonCortana";

            await voiceServiceConnection.RequestAppLaunchAsync(response);
        }
    }
}
