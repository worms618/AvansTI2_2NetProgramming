using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Shapes;

namespace UwpProject.Model
{
    public class SpecialPlace
    {
        public MapIcon Icon { get; }
        public Geofence Fence { get; }
        public MapPolygon Circle { get; private set; }

        public SpecialPlace(Geofence fence,MapIcon icon)
        {
            this.Fence = fence;
            this.Icon = icon;
            Icon.ZIndex = 2;                     
            MakeCircle(360);
        }

        private void MakeCircle(int amountOfPoints)
        {
            if(Fence.Geoshape is Geocircle)
            {
                var geoCircle = Fence.Geoshape as Geocircle;
                if(geoCircle != null)
                {
                    List<BasicGeoposition> points = new List<BasicGeoposition>();
                    double slice = 2 * Math.PI / amountOfPoints;
                    for(int i = 0; i < amountOfPoints; i++)
                    {
                        double angle = slice * i;
                        double lon = geoCircle.Center.Longitude + geoCircle.Radius * Math.Cos(angle);
                        double lat = geoCircle.Center.Latitude + geoCircle.Radius * Math.Sin(angle);
                        points.Add(new BasicGeoposition { Latitude = lat, Longitude = lon });
                    }

                    //longitude = x, latitude = y
                    Circle = MapElementFactory.MakePolygon(Colors.Gray,Colors.Black,points,1);
                }
            }            
        }

    }
}
