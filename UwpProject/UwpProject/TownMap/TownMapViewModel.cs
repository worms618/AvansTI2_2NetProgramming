using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

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

        private TownMapViewModel()
        {
            SetUpGeolocator();
        }

        private async void SetUpGeolocator()
        {
            var response = await Geolocator.RequestAccessAsync();
            switch (response)
            {
                case GeolocationAccessStatus.Allowed:
                    Debug.WriteLine("acces allowed");
                    _locator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                    break;
                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("Acces denied");
                    break;
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Acees unspecified");
                    break;
            }
        }
    }
}
