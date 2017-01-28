using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace BackgroundAPIs
{
    public sealed class Commands : IBackgroundTask
    {
        //fuck you cortana :|
        private VoiceCommandServiceConnection VoiceCommandServiceConnection { get; set; }
        public BackgroundTaskDeferral BackgroundTaskDeferral { get; set; }
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral = taskInstance.GetDeferral();
            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (triggerDetails != null && triggerDetails.Name.Equals("Bgcommand"))
            {
                VoiceCommandServiceConnection =
                    VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);

                VoiceCommandServiceConnection.VoiceCommandCompleted +=
                    VoiceCommandServiceConnection_VoiceCommandCompleted;
                var voiceCommand = await VoiceCommandServiceConnection.GetVoiceCommandAsync();

                switch (voiceCommand.CommandName)
                {
                    case "ToDoFetcher":
                        {
                             SendCompletionMessageForToDos();
                            
                            break;
                        }

                    // As a last resort, launch the app in the foreground.
                    default:
                        LaunchAppInForeground();
                        break;
                }

            }
            BackgroundTaskDeferral?.Complete();
        }
        private async void LaunchAppInForeground()
        {
            var userMessage = new VoiceCommandUserMessage();
            userMessage.SpokenMessage = "Launching Todos";

            var response = VoiceCommandResponse.CreateResponse(userMessage);

            // When launching the app in the foreground, pass an app 
            // specific launch parameter to indicate what page to show.
            response.AppLaunchArgument = "showAllTodos=true";

            await VoiceCommandServiceConnection.RequestAppLaunchAsync(response);
        }
        private async void SendCompletionMessageForToDos()
        {
            // Take action and determine when the next trip to destination
            // Insert code here.

            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.

            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            var userMessage = new VoiceCommandUserMessage();
            userMessage.DisplayMessage = "Here’s your Todos.";
            userMessage.SpokenMessage = "Your To Do List which I'm sure you do not do these";

            // Optionally, present visual information about the answer.
            // For this example, create a VoiceCommandContentTile with an 
            // icon and a string.
            var toDoContentTiles = new List<VoiceCommandContentTile>();

            var toDoTile = new VoiceCommandContentTile();
            toDoTile.ContentTileType =
                VoiceCommandContentTileType.TitleWith68x68IconAndText;
            // The user can tap on the visual content to launch the app. 
            // Pass in a launch argument to enable the app to deep link to a 
            // page relevant to the item displayed on the content tile.
            toDoTile.AppLaunchArgument =
                string.Format("Todo={0}”, “Pay Hesam");
            toDoTile.Title = "Payment";
            toDoTile.TextLine1 = "Today";
            toDoContentTiles.Add(toDoTile);

            // Create the VoiceCommandResponse from the userMessage and list    
            // of content tiles.
            var response =
                VoiceCommandResponse.CreateResponse(
                    userMessage, toDoContentTiles);

            // Cortana will present a “Go to app_name” link that the user 
            // can tap to launch the app. 
            // Pass in a launch to enable the app to deep link to a page 
            // relevant to the voice command.
            response.AppLaunchArgument =
                string.Format("ToDo={0}”, “Payment");

            // Ask Cortana to display the user message and content tile and 
            // also speak the user message.
            await VoiceCommandServiceConnection.ReportSuccessAsync(response);
        }
        private void VoiceCommandServiceConnection_VoiceCommandCompleted(VoiceCommandServiceConnection sender,
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
