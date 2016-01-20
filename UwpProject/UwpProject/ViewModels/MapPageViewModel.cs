using System;
using System.Diagnostics;
using UwpProject.Model;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
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
            set { SetMeters(value.Position); _currentLocation = value; }
        }

        private double meters = 0.0;
        public double Meters
        {
            get { return meters; }
            set { meters = value; OnPropertyChanged(nameof(meters)); }
        }

        public MapIcon CurrentLocationIcon { get; }

        private MapPageViewModel()
        {
            SetLocator();
            CurrentLocationIcon = MapElementFactory.MakeIcon(CurrentLocation, new Uri("ms-appx:///Assets/pokebal_16x16.png"), 10);
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

        private void SetMeters(BasicGeoposition newPoint)
        {
            if(CurrentLocation != null)
            {
                Meters += CalculateMeters(CurrentLocation.Position, newPoint);
                Debug.WriteLine($"Afstand gelopen: {Meters}");
            }
        }

        private double CalculateMeters(BasicGeoposition first, BasicGeoposition second)
        {
            double theta = first.Longitude - second.Longitude;
            double dist = Math.Sin(deg2rad(first.Latitude)) * Math.Sin(deg2rad(second.Latitude)) + Math.Cos(deg2rad(first.Latitude)) * Math.Cos(deg2rad(second.Latitude)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;            
            dist = dist * 1.609344; //naar kilometers
            dist = dist * 1000.0;//naar meters
            return (dist);
        } 

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
