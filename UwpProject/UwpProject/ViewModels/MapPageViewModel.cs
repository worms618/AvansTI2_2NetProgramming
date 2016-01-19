using System;
using System.Diagnostics;
using UwpProject.Model;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.ViewModels
{
    public class MapPageViewModel : PropertyChange
    {
        private static MapPageViewModel mpvm = null;
        public static MapPageViewModel Instance
        {
            get
            {
                if (mpvm == null)
                {
                    mpvm = new MapPageViewModel();
                }
                return mpvm;
            }
        }

        private Geolocator _locator;

        private Geopoint _currentLocation;
        public Geopoint CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        public MapIcon CurrentLocationIcon { get; }

        private MapPageViewModel()
        {
            SetLocator();
            CurrentLocationIcon = MapElementFactory.MakeIcon(CurrentLocation, new Uri("ms-appx:///Assets/pokebal_16x16.png"), 1);
        }

        public async void SetLocator()
        {
            var access = await Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Allowed:
                    _locator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };                                                                         
                    break;
                case GeolocationAccessStatus.Denied:                    
                    Debug.WriteLine("No access");                    
                    break;
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Unspecified access");
                    break;
            }
        }
        
        public async void SetCurrentLocation()
        {
            //Debug.WriteLine("getting location");
            if (_locator != null)
            {
                Geoposition pos = await _locator.GetGeopositionAsync();
                //Debug.WriteLine($"get location: {pos.Coordinate.Point.ToString()}");
                CurrentLocation = pos.Coordinate.Point;
                CurrentLocationIcon.Location = pos.Coordinate.Point;
            }
        }
    }
}
