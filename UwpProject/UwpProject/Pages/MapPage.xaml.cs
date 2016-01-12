using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private MapPageViewModel mpwm;

        public MapPage()
        {
            this.InitializeComponent();
            mpwm = MapPageViewModel.Instance;
            DataContext = mpwm;
        }

        private bool DeleteShapesFromLevel(int zIndex)
        {
            var shapesOnLevel = MapControler.MapElements.Where(p => p.ZIndex == zIndex);
            if (shapesOnLevel.Any())
            {
                foreach (var shape in MapControler.MapElements)
                {
                    MapControler.MapElements.Remove(shape);
                }
                return true;
            }
            return false;
        }

        private void MapControler_CenterChanged(Windows.UI.Xaml.Controls.Maps.MapControl sender, object args)
        {

        }
    }
}
