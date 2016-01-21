using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Model;
using UwpProject.ViewModels;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private MapPageViewModel mpwm;
        DispatcherTimer timer;
        bool onetime;        
        private List<SpecialPlace> specialPlaces;
        public MapPage()
        {
            this.InitializeComponent();
            mpwm = MapPageViewModel.Instance;
            specialPlaces = new List<SpecialPlace>();
            mpwm.SetCurrentLocation();
            DataContext = mpwm;
            
            BasicGeoposition bg = new BasicGeoposition { Latitude = 51.803404, Longitude = 4.899284, Altitude = 0.0 };
            MapIcon home = MapElementFactory.MakeIcon(new Geopoint(bg), new Uri("ms-appx:///Assets/home_16x16.png"), 1);
            
            Geocircle gc = new Geocircle(home.Location.Position, 25);
            MonitoredGeofenceStates states = MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited | MonitoredGeofenceStates.Removed | MonitoredGeofenceStates.None;
            Geofence gf = new Geofence("home", gc, states, false, TimeSpan.FromSeconds(1), DateTime.Now, TimeSpan.FromDays(1));
            specialPlaces.Add(new SpecialPlace(gf, home));
                        
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            onetime = false;
        }

        private async void Current_StatusChanged(GeofenceMonitor sender, object args)
        {
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Debug.WriteLine("status changed");
            });
        }

        private void Timer_Tick(object sender, object e)
        {
            //Debug.WriteLine("Tick");
            mpwm.SetCurrentLocation();
            //Debug.WriteLine($"Geofences count: {GeofenceMonitor.Current.Geofences.Count}");
            if (!onetime)
            {
                SetCurrentLocationCenter();
                onetime = true;
            }         
        }

        private bool DeleteShapesFromLevel(int zIndex)
        {
            var shapesOnLevel = MapControler.MapElements.Where(p => p.ZIndex == zIndex);
            if (shapesOnLevel.Any())
            {
                foreach (var shape in shapesOnLevel.ToList())
                {
                    MapControler.MapElements.Remove(shape);
                }
                return true;
            }
            return false;
        }
        
        private async void Current_GeofenceStateChanged(GeofenceMonitor sender, object args)
        {            
            var reports = sender.ReadReports();
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Debug.WriteLine("geofence state changed");
                foreach (GeofenceStateChangeReport report in reports)
                {
                    
                    switch (report.NewState)
                    {
                        case GeofenceState.Removed:
                            GeofenceMonitor.Current.Geofences.Remove(report.Geofence);
                            break;
                        case GeofenceState.Entered:
                            Debug.WriteLine($"Je bent binnen bereik van {report.Geofence.Id}");
                            break;
                        case GeofenceState.Exited:
                            Debug.WriteLine($"Je bent buiten bereik van {report.Geofence.Id}");
                            break;
                        case GeofenceState.None:
                            Debug.WriteLine($"Geen commetaar {report.Geofence.Id}");
                            break;
                    }
                }
            });
        }

        private void CurrentLocationPin_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentLocationCenter();
        }

        private void SetCurrentLocationCenter()
        {
            if(mpwm.CurrentLocation != null)
            MapControler.Center = mpwm.CurrentLocation;
        }

        private void Mainpage_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            
            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;
            GeofenceMonitor.Current.StatusChanged += Current_StatusChanged;

            MapControler.MapElements.Add(mpwm.CurrentLocationIcon);
           
            foreach (SpecialPlace sp in specialPlaces)
            {
                MapControler.MapElements.Add(sp.Icon);
                //MapControler.MapElements.Add(sp.Circle);                    
                if (GeofenceMonitor.Current.Geofences.FirstOrDefault(gf1 => gf1.Id == sp.Fence.Id) == null)
                {
                    GeofenceMonitor.Current.Geofences.Add(sp.Fence);
                }

            }
            timer.Start();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            GeofenceMonitor.Current.GeofenceStateChanged -= Current_GeofenceStateChanged;
            GeofenceMonitor.Current.StatusChanged -= Current_StatusChanged;
            timer.Stop();
            MapControler.MapElements.Clear();
            GeofenceMonitor.Current.Geofences.Clear();
        }
    }
}
