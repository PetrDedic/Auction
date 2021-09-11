using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Dokumentaci k šabloně položky Prázdná stránka najdete na adrese https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x405

namespace Aukce
{
    /// <summary>
    /// Prázdná stránka, která se dá použít samostatně nebo v rámci objektu Frame
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            frame.Navigate(typeof(AllAuctions));

            MaximizeWindowOnLoad();

            Windows.UI.ViewManagement.ApplicationViewTitleBar uwpTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;

            uwpTitleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;

            uwpTitleBar.BackgroundColor = Windows.UI.Colors.Transparent;



            //using Windows.ApplicationModel.Core

            Windows.ApplicationModel.Core.CoreApplicationViewTitleBar coreTitleBar = Windows.ApplicationModel.Core.CoreApplication.GetCurrentView().TitleBar;

            coreTitleBar.ExtendViewIntoTitleBar = true;

            //----</ Transparent Title >----

            void MaximizeWindowOnLoad()
            {
                var view = DisplayInformation.GetForCurrentView();

                // Get the screen resolution (APIs available from 14393 onward).
                var resolution = new Size(view.ScreenWidthInRawPixels, view.ScreenHeightInRawPixels);

                // Calculate the screen size in effective pixels. 
                // Note the height of the Windows Taskbar is ignored here since the app will only be given the maxium available size.
                var scale = view.ResolutionScale == ResolutionScale.Invalid ? 1 : view.RawPixelsPerViewPixel;
                var bounds = new Size(resolution.Width / scale, resolution.Height / scale);

                ApplicationView.PreferredLaunchViewSize = new Size(bounds.Width, bounds.Height);
                ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            }
        }

        private void AllAuctions_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(AllAuctions));
        }

        private void MyAuctions_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(MyAuctions));
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(Details));
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(typeof(Sign));
        }
    }
}
