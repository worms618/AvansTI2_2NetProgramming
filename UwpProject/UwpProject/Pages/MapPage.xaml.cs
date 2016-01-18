using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Model;
using UwpProject.ViewModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
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
        public MapPage()
        {
            this.InitializeComponent();
            mpwm = MapPageViewModel.Instance;
            mpwm.SetCurrentLocation();
            DataContext = mpwm;
            MapControler.MapElements.Add(mpwm.CurrentLocationIcon);                       

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            onetime = false;
        }
        
        private void Timer_Tick(object sender, object e)
        {
            //Debug.WriteLine("Tick");
            mpwm.SetCurrentLocation();
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
        
        private void MapControler_CenterChanged(MapControl sender, object args)
        {                          
            DeleteShapesFromLevel(2);            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            timer.Start();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            timer.Start();
            MapControler.MapElements.Clear();
        }

        private void CurrentLocationPin_Click(object sender, RoutedEventArgs e)
        {
            SetCurrentLocationCenter();
        }

        private void SetCurrentLocationCenter()
        {
            MapControler.Center = mpwm.CurrentLocation;
        }
    }
}
