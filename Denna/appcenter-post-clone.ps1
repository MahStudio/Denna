echo "Init"

echo $env:DennaURL

echo $env:APPCENTER_SOURCE_DIRECTORY

$var | Out-File  $env:APPCENTER_SOURCE_DIRECTORY\Denna\Core\Constants.cs

Set-Content -Path  $env:APPCENTER_SOURCE_DIRECTORY\Denna\Core\Constants.cs "using System;
namespace Core
{
    public static class Constants
    {
        public static readonly Uri ServerUri = new Uri( `" $env:DennaURL`" );
    }
}"
