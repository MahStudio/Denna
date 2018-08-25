﻿using Core.Data;
using Core.Service.Users;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Denna.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;
        bool IsLoggedIn = false;
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            IsLoggedIn = UserService.IsUserLoggenIn();
            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This ensures that the extended splash screen formats properly in response to window resizing.
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            splash = splashscreen;
            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
            }

            // Create a Frame to act as the navigation context
            rootFrame = new Frame();

        }

        void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;

            // Complete app setup operations here...
        }

        void DismissExtendedSplash()
        {
            if (IsLoggedIn)
                rootFrame.Navigate(typeof(PageMaster));
            else
                rootFrame.Navigate(typeof(Welcome));

            // Place the frame in the current Window
            Window.Current.Content = rootFrame;
        }

        async void RestoreStateAsync(bool loadState)
        {
            if (loadState)
            {
                // code to load your app's state here
            }
        }

        void Media_MediaEnded(object sender, RoutedEventArgs e) => DismissExtendedSplash();
    }
}