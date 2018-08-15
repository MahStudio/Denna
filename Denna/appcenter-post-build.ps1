$megtxt="Denna insider build $env:APPCENTER_BUILD_ID on $env:APPCENTER_BRANCH is now available at: 
https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/Insiders

and full package including dependencies and certificate is here:
https://github.com/MahStudio/Denna/releases/latest

Any issues? Post is here:
https://github.com/MahStudio/Denna/issues

or you can join Denna insider chat:
https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g
"
echo $megtxt;
Invoke-WebRequest -Uri "https://api.telegram.org/bot$env:BotSecret/sendMessage?chat_id=$env:chatId&text=$megtxt"
Invoke-WebRequest -Uri "https://api.telegram.org/bot$env:BotSecret/sendMessage?chat_id=$env:ChannelId&text=$megtxt"


$versionNumber = $env:APPCENTER_BUILD_ID
$preRelease = $TRUE
$releaseNotes=$megtxt
$artifactOutputDirectory="$env:APPCENTER_SOURCE_DIRECTORY\Denna\Denna\AppPackages"

$gitHubUsername="MahStudio"
$gitHubRepository="Denna"
$gitHubApiKey="$env:GithubSicktear"
    $draft = $FALSE
    
    $releaseData = @{
       tag_name = [string]::Format("Build {0}", $versionNumber);
       name = [string]::Format("Build {0}", $versionNumber);
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
    $include = @("*.appxbundle","*.cer","*.appx")
    $removefiles = Get-ChildItem "$artifactOutputDirectory" -recurse -force -include $include | % { $_.FullName }

    $uploadUri = $result | Select -ExpandProperty upload_url
    Write-Host $uploadUri
    $uploadUri = $uploadUri -creplace '\{\?name,label\}'  #, "?name=$artifact"
    
foreach ($file in $removefiles) {
    $outputFile = Split-Path $file -leaf
    $parent = (get-item $file ).parent
    $uploadParams = @{
      Uri = $uploadUri + "?name=$parent$outputFile";
      Method = 'POST';
      Headers = @{
        Authorization = $auth;
      }
      ContentType = 'application/zip';
      InFile = $file
    }
    $result = Invoke-RestMethod @uploadParams
}
