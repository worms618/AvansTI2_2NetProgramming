using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UwpProject.Model.Places
{
    public class PokeCenter : SpecialPlace
    {
        public PokeCenter(string id,BasicGeoposition position):base(id,position, new Uri("ms-appx:///Assets/home_16x16_1.png"),new Uri("ms-appx:///Assets/home_16x16_2.png"))
        { }
    }
}
