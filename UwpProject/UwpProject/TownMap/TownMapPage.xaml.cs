using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpProject.TownMap
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TownMapPage : Page
    {
        private TownMapViewModel tmvm;
        private DispatcherTimer timer;
        private bool firstTime = false;

        public TownMapPage()
        {
            this.InitializeComponent();
            tmvm = TownMapViewModel.Instance;
            timer = new DispatcherTimer();
            DataContext = tmvm;

            timer.Interval = TimeSpan.FromSeconds(1);//wordt elke seconde getriggerd.
            timer.Tick += Timer_Tick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "TownMap";
            MapController.MapElements.Add(tmvm.LocationIcon);
            timer.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Stop();
            MapController.MapElements.Clear();
            base.OnNavigatedFrom(e);
        }

        private void Timer_Tick(object sender, object e)
        {
            if(tmvm.CurrentLocation != null && !firstTime)
            {
                MapController.Center = tmvm.CurrentLocation;
                MapController.ZoomLevel = 18;
                firstTime = true;
            }
            tmvm.SetCurrentLocation();
        }        
    }
}
