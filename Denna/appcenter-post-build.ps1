$megtxt="Denna insider build $env:APPCENTER_BUILD_ID on $env:APPCENTER_BRANCH is now available at: https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/Insiders"
echo $megtxt;
Invoke-WebRequest -Uri "https://api.telegram.org/bot$env:BotSecret/sendMessage?chat_id=$env:chatId&text=$megtxt"
