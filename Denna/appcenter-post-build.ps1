$megtxt="Denna insider build $env:APPCENTER_BUILD_ID on $env:APPCENTER_BRANCH is now available at: 
https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/Insiders

Any issues? Post is here:
https://github.com/MahStudio/Denna/issues

or you can join Denna insider chat:
https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g
"
echo $megtxt;
Invoke-WebRequest -Uri "https://api.telegram.org/bot$env:BotSecret/sendMessage?chat_id=$env:chatId&text=$megtxt"
Invoke-WebRequest -Uri "https://api.telegram.org/bot$env:BotSecret/sendMessage?chat_id=$env:ChannelId&text=$megtxt"
