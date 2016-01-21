using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UwpProject.Model
{
    public class WalkKeeper
    {
        public event EventHandler EncounterTrigger;
        private  void EncouterTriggerd()
        {
            var handler = EncounterTrigger;
            if(handler != null)
            {
                handler(null, new EventArgs());
            }
        }

        private double _totalMeters = 0;
        public double EncounterMeters { get; }

        public WalkKeeper()
        {
            EncounterMeters = 5.0;
        }

        public void Walk(BasicGeoposition old, BasicGeoposition New)
        {
            _totalMeters += CalculateMeters(old, New);
            if(_totalMeters >= EncounterMeters)
            {
                EncouterTriggerd();
                _totalMeters = 0.0;
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
