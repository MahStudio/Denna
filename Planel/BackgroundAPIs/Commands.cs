using Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace Planel.BackgroundAPIs
{
    public sealed class Commands : IBackgroundTask
    {
        //fuck you cortana :|
        private BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            //Take a service deferral so the service isn&#39;t terminated.
            this.serviceDeferral = taskInstance.GetDeferral();

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

                    VoiceCommand voiceCommand = await
                        voiceServiceConnection.GetVoiceCommandAsync();
                    switch (voiceCommand.CommandName)
                    {
                        case "ToDoFetcher":
                            {
                                SendCompletionMessageFortodolist();
                                break;
                            }
                        case "HobbyFetcher":
                            {
                                fetchhobby();
                                break;
                            }
                        case "TodoCounter":
                            {
                                taskcounter();
                                break;
                            }
                        case "HobbyCounter":
                            {
                                hobbycounter();
                                break;
                            }
                        case "addhobby":
                            {
                                LaunchAppInForeground();
                                    break;
                            }
                        case "addtask":
                            {
                                LaunchAppInForeground();
                                break;
                            }
                        case "getperformance":
                            {
                                getperf();
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
                    if (this.serviceDeferral != null)
                    {
                        // Complete the service deferral.
                        this.serviceDeferral.Complete();
                    }
                }
            }
        }
        private void VoiceCommandCompleted(
           VoiceCommandServiceConnection sender,
           VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                // Insert your code here.
                // Complete the service deferral.
                this.serviceDeferral.Complete();
            }
        }
        private async void getperf()
        {
            var z = Core.Models.Localdb.percentage();

            VoiceCommandResponse response = null;
            
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "There's your performance";
                userMessage.SpokenMessage = "You have done" + z.firstpercentage+ " pescent and postponed" +z.firstsuspend + "percent of your tasks today." + "and You have done" + z.secondpercentage + " pescent and postponed" + z.secondsuspend + "percent of your tasks Yesterday.";
                response =
                 VoiceCommandResponse.CreateResponse(userMessage);
           
            



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
        
      private void Task_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            serviceDeferral.Complete();
        }

        private void TaskInstance_Canceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            serviceDeferral.Complete();
        }
        private async void hobbycounter()
        {
            ObservableCollection<Core.Models.Hobby> mycol = new ObservableCollection<Core.Models.Hobby>();
            mycol = Core.Models.Localdb.Gethobbies();
            VoiceCommandResponse response = null;
            if (mycol.Count == 0)
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "No hobbies on Denna";
                userMessage.SpokenMessage = "You have no hobbies ! Add one";
                response =
                 VoiceCommandResponse.CreateResponse(userMessage);
            }
            else
            {
                var userMessage = new VoiceCommandUserMessage();
                var x = mycol.Count;
                userMessage.DisplayMessage = "You have " + x + " hobbies" + " on Denna";
                userMessage.SpokenMessage = "You have " + x + " hobbbies";

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
        private async void taskcounter()
        {
            var x = Localdb.counter();
            VoiceCommandResponse response = null;
            if (x==0)
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
                userMessage.DisplayMessage = "You have "+ x +" tasks"+" on Denna";
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
        private async void fetchhobby()
        {
            // Take action and determine when the next trip to destination
            // Insert code here.

            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.

            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            

            // Optionally, present visual information about the answer.
            // For this example, create a VoiceCommandContentTile with an 
            // icon and a string.
            var destinationsContentTiles = new List<VoiceCommandContentTile>();


            // The user can tap on the visual content to launch the app. 
            // Pass in a launch argument to enable the app to deep link to a 
            // page relevant to the item displayed on the content tile.

            ObservableCollection<Core.Models.Hobby> mycol = new ObservableCollection<Core.Models.Hobby>();
            mycol = Core.Models.Localdb.Gethobbies();
            VoiceCommandResponse response = null;
            if (mycol.Count == 0)
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "No hobbies on Denna";
                userMessage.SpokenMessage = "You have no hobbies ! Add one";
                response =
                 VoiceCommandResponse.CreateResponse( userMessage);
            }
            else
            {
                var userMessage = new VoiceCommandUserMessage();
                userMessage.DisplayMessage = "Here's your hobbbies on Denna";
                userMessage.SpokenMessage = "Here's your hobbies";
                foreach (var item in mycol)
                {
                    var destinationTile = new VoiceCommandContentTile();
                    destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWithText;
                    destinationTile.AppLaunchArgument = "agsonCortana";
                    destinationTile.Title = item.title;
                    destinationTile.TextLine1 = item.detail;
                    destinationTile.TextLine2 = convertor(item.Days);
                    destinationTile.TextLine3 = item.time.ToString();
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
        private string convertor(string json)
        {
            
            var toadd = JsonConvert.DeserializeObject<IList<DayOfWeek>>(json);
            string Days = "";
            if (toadd.Count == 7)
            {
                Days = "Every Day";
            }
            else
            {
                foreach (var item in toadd)
                {
                    Days += item.ToString() + "  ";
                }
            }


            return Days;
        }
        private async void SendCompletionMessageFortodolist(  )
        {
            // Take action and determine when the next trip to destination
            // Insert code here.

            // Replace the hardcoded strings used here with strings 
            // appropriate for your application.

            // First, create the VoiceCommandUserMessage with the strings 
            // that Cortana will show and speak.
            

            // Optionally, present visual information about the answer.
            // For this example, create a VoiceCommandContentTile with an 
            // icon and a string.
            var destinationsContentTiles = new List<VoiceCommandContentTile>();

           
            // The user can tap on the visual content to launch the app. 
            // Pass in a launch argument to enable the app to deep link to a 
            // page relevant to the item displayed on the content tile.
            
            ObservableCollection<Core.Models.todo> mycol = new ObservableCollection<Core.Models.todo>();
            mycol = Core.Models.Localdb.getall(DateTime.Now.ToLocalTime());
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
                    destinationTile.Title = item.title;
                    destinationTile.TextLine1 = item.detail;
                    destinationTile.TextLine2 = item.time.ToString();
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

        private async void LaunchAppInForeground()
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
