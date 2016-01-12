using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpProject.Model;
using Windows.Devices.Geolocation;

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
        public Geolocator Locator
        {
            get { return _locator; }
            private set { _locator = value; OnPropertyChanged(nameof(Locator)); }
        }

        private Geopoint _currentLocation;
        public Geopoint CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; OnPropertyChanged(nameof(CurrentLocation)); }
        }

        private MapPageViewModel()
        {
            SetLocator();
        }

        public async void SetLocator()
        {
            var access = await Geolocator.RequestAccessAsync();
            switch (access)
            {
                case GeolocationAccessStatus.Allowed:
                    _locator = new Geolocator() { DesiredAccuracyInMeters = 2 };
                    Geoposition pos = await _locator.GetGeopositionAsync();
                    CurrentLocation = pos.Coordinate.Point;
                    break;
                case GeolocationAccessStatus.Denied:                    
                    Debug.WriteLine("No access");
                    //SetLocator();
                    break;
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Unspecified access");
                    break;
            }
        }
    }
}
