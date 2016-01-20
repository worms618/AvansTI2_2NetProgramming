using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.Model
{
    public class MapElementFactory
    {
        public static MapIcon MakeIcon(Geopoint point, Uri imageUri, int zIndex)
        {
            var anchorPoint = new Point(0.5, 0.5);
            try
            {
                var pokebalImage = RandomAccessStreamReference.CreateFromUri(imageUri);
                var shape = new MapIcon
                {
                    Location = point,
                    NormalizedAnchorPoint = anchorPoint,
                    Image = pokebalImage,
                    CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible,
                    ZIndex = zIndex
                };
                return shape;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        public static MapPolygon MakePolygon(Color fillColor,Color strokecolor,List<BasicGeoposition> positions,int zIndex)
        {
            var shape = new MapPolygon
            {
                FillColor = fillColor,
                StrokeColor = strokecolor,
                Path = new Geopath(positions),
                ZIndex = zIndex,
                Visible = true
            };
            return shape;
        }
    }
}
