# Denna

[![Build status](https://build.appcenter.ms/v0.1/apps/dc393a1e-1121-4a3e-99c2-589182d9e5f6/branches/master/badge)](https://appcenter.ms)

Denna is a fully featured and productive To-Do list for Windows 10 devices. [Get it from Microsoft Store](https://www.microsoft.com/en-us/store/p/denna/9n9c2hwnzcft)

## Story

Denna's idea came from my own needs. I'm not punctual at all, so I needed a tool to make me care more about the time I have.
So with the help of the whole team in MahStudio we came across with a project named *Project Plannel* which then became **Denna**.

**Denna** is a name of a mountain in Iran. The idea of *Denna* came from where I failed my test due to not making time for studies and the answer of that missed question was Denna.

Then with the help of the team and the community we developed and release a lovely tool named **Denna**.
The aim of Denna is to help people to care more about the time they have. Cuz time flies!

![image](https://user-images.githubusercontent.com/22152065/38919555-2566f7b2-4306-11e8-8a9c-95eb08eff28a.png)

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
![image](https://user-images.githubusercontent.com/22152065/38919395-b3181f38-4305-11e8-8ead-161766d6148c.png)

![image](https://user-images.githubusercontent.com/22152065/38919478-f123c7b4-4305-11e8-9d7c-98d49b692b31.png)

Also you can see the whole design files [HERE](https://github.com/MahStudio/Denna/tree/master/Design)

## Contributing

Denna looks for new maintainers and contributors. The key feature that we need for next update is sync and the features that are into the design.
As Denna is an open source project, all kind of contributions are welcome.

> **Note:** Denna V2 is under development. You can help us by giving us feedbacks. Nightly builds are [HERE AT DENNA INSIDER CHAT ON TELEGRAM](https://t.me/joinchat/EJ9gLURDAcbOxpoIGSdD8g)

### What we had in V1

- Manage your tasks
- Cortana interaction
- Action center actions
- Manage your hobbies
- Ability to postpone,edit and delete your tasks
- Share tasks
- Back up tasks
- Graphical performance charts

### What features are planned in V2

- Offline-first Sync
- New codebase
- Better performance
- Design improvements
- Support for android and IOS
- and so on

You can see V 2.0 milestone [HERE](https://github.com/MahStudio/Denna/milestone/1)

### Build Prerequisites

1. Windows 10
2. Visual Studio 2017 (latest build) with universal windows development features installed.
3. GIT for Windows ([install from here](http://gitforwindows.org/))
4. Knowledge about C#, Xaml, [Realm](https://realm.io/), MVVM, design patterns and Windows development

### Build instructions

We use realm as our database and sync engine, so see [Realm.net documentation](https://realm.io/docs/dotnet/latest/) first.

1. Clone the repo
2. Open Denna Solution
3. Open [Realm cloud website](https://cloud.realm.io/) and register for a realm instance for free.
4. Back into solution, there is a missing `Constants.cs` file. Replace it with following code. Remember that you should paste your *realm instance URL* in `ServerUri`.

```csharp
using System;
namespace Core
{
    public static class Constants
    {
        public static readonly Uri ServerUri = new Uri("My realm server URL");
    }
}
```

5. Hit F5 and run the project

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
