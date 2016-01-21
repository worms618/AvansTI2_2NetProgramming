using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpProject.Model;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.TownMap
{
    public class TownMapViewModel
    {
        private static TownMapViewModel _instance;
        public static TownMapViewModel Instance
        {
            get {
                    if(_instance == null)
                    {
                        _instance = new TownMapViewModel();
                    }
                    return _instance;
                }            
        }

        private Geolocator _locator;

        private Geopoint _currentLocation;
        public Geopoint CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; LocationIcon.Location = value; }
        }

        public MapIcon LocationIcon { get; }

        private TownMapViewModel()
        {
            LocationIcon = MapElementFactory.MakeMapIcon(null, new Uri("ms-appx:///Assets/pokebal_16x16.png"), 1);
            SetUpGeolocator();            
        }

        private async void SetUpGeolocator()
        {
            var response = await Geolocator.RequestAccessAsync();
            switch (response)
            {
                case GeolocationAccessStatus.Allowed:
                    //Debug.WriteLine("acces allowed");
                    _locator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                    SetCurrentLocation();
                    break;
                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("Acces denied");
                    _locator = null;
                    break;
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Acees unspecified");
                    _locator = null;
                    break;
            }
        }

        public async void SetCurrentLocation()
        {
            if(_locator != null)
            {
                var position = await _locator.GetGeopositionAsync();
                if (position != null)
                {
                    CurrentLocation = position.Coordinate.Point;                    
                    //Debug.WriteLine($"Currentlocation Latitude: {CurrentLocation.Position.Latitude},Longitude: {CurrentLocation.Position.Longitude}");
                    //Debug.WriteLine($"Mapicon Latitude: {LocationIcon.Location.Position.Latitude},Longitude: {LocationIcon.Location.Position.Longitude}");
                }
            }            
        }
    }
}
