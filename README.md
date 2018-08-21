<p align="center">
  <a href="https://www.microsoft.com/en-us/store/p/denna/9n9c2hwnzcft">
    <img src="https://github.com/MahStudio/Denna/raw/master/Design/Denna%20logo%20Green.png" width=80 height=80>
  </a>

  <h3 align="center">Denna</h3>

  <p align="center">
    Denna is a fully featured and productive To-Do list for Windows 10 devices.
    <br>
    <a href="https://www.microsoft.com/en-us/store/p/denna/9n9c2hwnzcft">Download</a>
    &middot;
    <a href="https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g">Insiders Group</a>
  &middot;
    <a href="https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/insiders">Nightly builds</a>
    <br>
    <br>
  <a href="https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/insiders">
    <img src="https://build.appcenter.ms/v0.1/apps/dc393a1e-1121-4a3e-99c2-589182d9e5f6/branches/master/badge">
    </a>
  <a href="https://www.paypal.me/mohsens22">
    <img src="https://img.shields.io/badge/Donate-Paypal-blue.svg" />
  </a>
  <a href="https://waffle.io/MahStudio/Denna">
    <img src="https://badge.waffle.io/MahStudio/Denna.svg" />
  </a>
  </p>
</p>

<br>


## Story

Denna's idea came from my own needs. I'm not punctual at all, so I needed a tool to make me care more about the time I have.
So with the help of the whole team in MahStudio we came across with a project named *Project Plannel* which then became **Denna**.

**Denna** is a name of a mountain in Iran. The idea of *Denna* came from where I failed my test due to not making time for studies and the answer of that missed question was Denna.

Then with the help of the team and the community we developed and release a lovely tool named **Denna**.
The aim of Denna is to help people to care more about the time they have. Cuz time flies!

Ease of access and productivity is Denna's core value.

<p align="center">
    <img src="https://user-images.githubusercontent.com/22152065/38919555-2566f7b2-4306-11e8-8a9c-95eb08eff28a.png">
  </p>

### Denna in press

Denna got a really good attention from users and some of the news websites. You can read some of Denna's reviews down below:

- [Windows Central](https://www.windowscentral.com/denna-windows-10-do-list-improving-productivity)
- [On MSFT](https://www.onmsft.com/news/denna-is-a-well-rounded-universal-task-management-app-for-windows-10)
- [All about windows phone](http://allaboutwindowsphone.com/flow/item/22075_Denna_UWP.php)
- [MS Power User](https://mspoweruser.com/developer-submission-denna-uwp-todo-list-app/)
- [The win central](https://thewincentral.com/denna-windows-phone-goes-free-myappfree-app-day/)
- [Windows phone arena](http://www.windowsphonearea.com/3-new-windows-10-apps-march-2017/)

And so on ...

### Design

This project is designed by Sr. architect and designer Aref M. Ahmadi and Sr. designer Mohammad R. Alidoost.
<p align="center">
    <img src="https://user-images.githubusercontent.com/22152065/38919395-b3181f38-4305-11e8-8ead-161766d6148c.png">
    <br>
  <img src="https://user-images.githubusercontent.com/22152065/38919478-f123c7b4-4305-11e8-9d7c-98d49b692b31.png">
  </p>


Also you can see the whole design files [HERE](https://github.com/MahStudio/Denna/tree/master/Design)

## Contributing

Denna looks for new maintainers and contributors. The key feature that we need for next update is sync and the features that are into the design.
As Denna is an open source project, all kind of contributions are welcome.

### How do I contribute

You can contribute us in either 3 ways:

1. You can get our stable builds and send us feedbacks, ideas and feature requests either in email or [issues](https://github.com/MahStudio/Denna/issues)
2. You can get our nightly builds from [HERE](https://install.appcenter.ms/orgs/mahstudio-u5ev/apps/denna/distribution_groups/insiders) and give us feedback pr join us [HERE AT DENNA INSIDER CHAT ON TELEGRAM](https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g)
3. You can help us in development cycle.
4. You can get me a coffee!

<p align="center">
<a href="http://paypal.me/mohsens22">
    <img src="https://user-images.githubusercontent.com/22152065/43985552-5eb38708-9d1d-11e8-85ee-609299bcf0fc.png">
    </a>
  </p>


### What we offer in Denna

- Productive task management
- Offline-first sync
- Cortana interaction
- Action center integration 
- Productivity analysis

You can see V 2.1 milestone [HERE](https://github.com/MahStudio/Denna/milestone/2)

### Build Prerequisites

1. Windows 10
2. Visual Studio 2017 (latest build) with universal windows development features installed.
3. GIT for Windows ([install from here](http://gitforwindows.org/))
4. Knowledge about C#, Xaml, [Realm](https://realm.io/), MVVM, design patterns and Windows development

### Build instructions

We use realm as our database and sync engine, so see [Realm.net documentation](https://realm.io/docs/dotnet/latest/) first.

1. Clone the repo
2. Open Denna Solution
3. Open [Realm cloud website](https://cloud.realm.io/) and register for a realm instance for free. If you don't want sync, skip this step and open an [issue](https://github.com/MahStudio/Denna/issues) and ask us for the solution.
4. Back into solution, there is a missing `Constants.cs` file. Replace it with following code. Remember that you should paste your *realm instance URL* in `ServerUri`.
5. Register for an app in Microsoft App Center and paste your *app secret code* in `AppCenterSecret`. If you don't want analytics you can skip this and write a random string there.

```csharp
using System;
namespace Core
{
    public static class Constants
    {
        public static readonly Uri ServerUri = new Uri("My realm server URL");
        public static readonly string AppCenterSecret = "Your App Center secret" ;
    }
}
```

6. Hit F5 and run the project

### Authors

This project is designed, developed, maintained and supported by the community software development team **Mah Studio**.
See also the list of [contributors](https://github.com/MahStudio/Denna/contributors) who participated in this project.

#### Personnel

- [Mohsen Seifi](https://github.com/mohsens22) : Frontman, architect and lead developer.
- [Aref M. Ahmadi](https://www.instagram.com/itsaref/) : UI/UX design
- [Mohammad R. Alidoost](https://www.instagram.com/mr.alidoost/) : UI/UX design
- [Amir Stephade](https://www.instagram.com/amir_stefad/) : Motion
- Lukas Frensel : UX tests
- Reza Cloner : UX tests

Special thanks to AliNGame, Pouria Riahi, Windows insiders and developers community for their help to this project.

### Sponsor

This project is sponsored by [Realm.io](https://realm.io). 

### Metrics

[![Throughput Graph](http://graphs.waffle.io/MahStudio/Denna/throughput.svg)](https://waffle.io/MahStudio/Denna/metrics)

## Licence

This application is available under [GPL-3 Licence](https://github.com/MahStudio/Denna/blob/master/LICENSE).

Copyright © 2016-2018 [Denna Authors](https://github.com/Mahstudio/Denna/graphs/contributors) // [Mah Studio](https://github.com/Mahstudio/).
