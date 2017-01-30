using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace Panel.BackgroundAPIs
{
    public sealed class Commands : IBackgroundTask
    {
        //fuck you cortana :|
        private VoiceCommandServiceConnection VoiceCommandServiceConnection { get; set; }
        private BackgroundTaskDeferral BackgroundTaskDeferral { get; set; }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            Debug.WriteLine(cost);
            try
            {
                var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
                if (triggerDetails != null && triggerDetails.Name.Equals("Bgcommand"))
                {
                    VoiceCommandServiceConnection =
                        VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

                    VoiceCommandServiceConnection.VoiceCommandCompleted +=
                        VoiceCommand_Completed;

                    var voiceCommand = await VoiceCommandServiceConnection.GetVoiceCommandAsync();

                    switch (voiceCommand.CommandName)
                    {
                        case "ToDoFetcher":
                            {
                                var date = voiceCommand.Properties["date"][0];
                                await SendCompletionMessageForToDos(date);

                                break;
                            }

                        // As a last resort, launch the app in the foreground.
                        default:
                            LaunchAppInForeground();
                            break;
                    }

                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.ToString());
            }
            finally
            {
                BackgroundTaskDeferral?.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            BackgroundTaskDeferral?.Complete();
        }

        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Todos";
            userMessage.DisplayMessage = "Launching Todos";

            var response = VoiceCommandResponse.CreateResponse(userMessage);

            // When launching the app in the foreground, pass an app 
            // specific launch parameter to indicate what page to show.
            response.AppLaunchArgument = "showAllTodos=true";

            await VoiceCommandServiceConnection.RequestAppLaunchAsync(response);
        }
        private async Task SendCompletionMessageForToDos(string date)
        {
            // Take action and determine when the next trip to destination
            // Insert code here.

            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.

            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = "Here’s your to do";
            userMessage.SpokenMessage = $"Your to do list for {date}";

            // Optionally, present visual information about the answer.
            // For this example, create a VoiceCommandContentTile with an 
            // icon and a string.
            var destinationsContentTiles = new List<VoiceCommandContentTile>();

            var destinationTile = new VoiceCommandContentTile();
            destinationTile.ContentTileType =
              VoiceCommandContentTileType.TitleWith68x68IconAndText;
            // The user can tap on the visual content to launch the app. 
            // Pass in a launch argument to enable the app to deep link to a 
            // page relevant to the item displayed on the content tile.
            destinationTile.AppLaunchArgument = $"date={date}";
            destinationTile.Title = $"{date}";
            destinationTile.TextLine1 = "Do this now";
            destinationsContentTiles.Add(destinationTile);

            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.
            var response =
              VoiceCommandResponse.CreateResponse(
     userMessage, destinationsContentTiles);

            // Cortana will present a “Go to app_name” link that the user 
            // can tap to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument = $"date={date}";

            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await Task.Delay(4000);
            await VoiceCommandServiceConnection.ReportSuccessAsync(response);

        }
        private void VoiceCommand_Completed(VoiceCommandServiceConnection sender,
        VoiceCommandCompletedEventArgs args)
        {
            if (BackgroundTaskDeferral != null)
            {
                // Insert your code here.
                // Complete the service deferral.
                BackgroundTaskDeferral.Complete();
            }
        }
    }
}
