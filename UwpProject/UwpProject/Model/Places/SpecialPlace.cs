using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.Model
{
    public abstract class SpecialPlace
    {
        public string Id { get; }
        public Geofence Fence { get; }
        public MapIcon Icon { get; }

        private bool _inSide;
        public bool inSide
        {
            get { return _inSide; }
            set { _inSide = value; SetImage(); }
        }
        private Uri state1, state2;
        public SpecialPlace(string id,BasicGeoposition point,Uri state1,Uri state2)
        {
            this.Id = id;
            Geocircle area = new Geocircle(point, 25);
            MonitoredGeofenceStates states = MonitoredGeofenceStates.Entered | MonitoredGeofenceStates.Exited;
            Fence = new Geofence(Id, area, states, false, TimeSpan.FromSeconds(1));

            Geofence exsists = GeofenceMonitor.Current.Geofences.FirstOrDefault(f => f.Id == Fence.Id);
            if(exsists != null)
            {
                GeofenceMonitor.Current.Geofences.Remove(exsists);
            }
            GeofenceMonitor.Current.Geofences.Add(Fence);

            Icon = MapElementFactory.MakeMapIcon(new Geopoint(point), state1, 1);
            this.state1 = state1;
            this.state2 = state2;
        }
  
        private void SetImage()
        {            
            if (!inSide)
            {
                Icon.Image = RandomAccessStreamReference.CreateFromUri(state1);
            }
            else
            {
                Icon.Image = RandomAccessStreamReference.CreateFromUri(state2);
            }
        }

    }
}
