using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.Model
{
    public class MapElementFactory
    {
        public static MapIcon MakeMapIcon(Geopoint point,Uri imageUri,int zIndex)
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
    }
}
