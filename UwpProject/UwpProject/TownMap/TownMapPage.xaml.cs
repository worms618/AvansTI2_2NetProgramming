using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Model;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
            InitializeComponent();
            tmvm = TownMapViewModel.Instance;
            timer = new DispatcherTimer();
            DataContext = tmvm;

            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;

            timer.Interval = TimeSpan.FromSeconds(1);//wordt elke seconde getriggerd.
            timer.Tick += Timer_Tick;            
        }

        private async void Current_GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            Debug.WriteLine("Geofence state changed");
            var reports = sender.ReadReports();
            foreach(GeofenceStateChangeReport report in reports)
            {
                GeofenceState state = report.NewState;
                Geofence geofence = report.Geofence;
                SpecialPlace specialPlace = tmvm.SpecialPlaces.Where(s => s.Id == geofence.Id).FirstOrDefault();
                Debug.WriteLine($"fence id: {geofence.Id}");
                if(state == GeofenceState.Removed)
                {
                    GeofenceMonitor.Current.Geofences.Remove(geofence);
                }
                else if(state == GeofenceState.Entered)
                {
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                     {
                         specialPlace.inSide = true;
                         Debug.WriteLine("ik ben binnen huis bereik :$");
                     });
                }else if(state == GeofenceState.Exited)
                {

                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        specialPlace.inSide = false;
                        Debug.WriteLine("Ik ben niet meer binnen huis bereik :(");
                    });                    
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "TownMap";
            MapController.MapElements.Add(tmvm.LocationIcon);
            foreach(SpecialPlace sp in tmvm.SpecialPlaces)
            {
                MapController.MapElements.Add(sp.Icon);
            }           
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
