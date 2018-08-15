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


Compress-Archive -path "$env:APPCENTER_SOURCE_DIRECTORY\Denna\Denna\AppPackages" -DestinationPath "$env:APPCENTER_SOURCE_DIRECTORY\Denna\Denna\Build.zip"



$versionNumber = $env:APPCENTER_BUILD_ID
$preRelease = $TRUE
$releaseNotes=$megtxt
$artifactOutputDirectory="$env:APPCENTER_SOURCE_DIRECTORY\Denna\Denna"
$artifact="Build.zip"
$gitHubUsername="MahStudio"
$gitHubRepository="Denna"
$gitHubApiKey="$env:GithubSicktear"
    $draft = $FALSE
    
    $releaseData = @{
       tag_name = [string]::Format("v{0}", $versionNumber);
       name = [string]::Format("v{0}", $versionNumber);
       body = $releaseNotes;
       draft = $draft;
       prerelease = $preRelease;
    }

    $auth = 'Basic ' + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes($gitHubApiKey + ":x-oauth-basic"));

    $releaseParams = @{
       Uri = "https://api.github.com/repos/$gitHubUsername/$gitHubRepository/releases";
       Method = 'POST';
       Headers = @{
         Authorization = $auth;
       }
       ContentType = 'application/json';
       Body = (ConvertTo-Json $releaseData -Compress)
    }

    $result = Invoke-RestMethod @releaseParams 
    $uploadUri = $result | Select -ExpandProperty upload_url
    Write-Host $uploadUri
    $uploadUri = $uploadUri -creplace '\{\?name,label\}'  #, "?name=$artifact"
    $uploadUri = $uploadUri + "?name=$artifact"
    $uploadFile = Join-Path -path $artifactOutputDirectory -childpath $artifact

    $uploadParams = @{
      Uri = $uploadUri;
      Method = 'POST';
      Headers = @{
        Authorization = $auth;
      }
      ContentType = 'application/zip';
      InFile = $uploadFile
    }
    $result = Invoke-RestMethod @uploadParams
