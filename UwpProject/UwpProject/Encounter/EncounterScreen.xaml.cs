using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Data;
using UwpProject.TownMap;
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

namespace UwpProject.Encounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EncounterScreen : Page
    {
        private EncounterScreenViewModel esvm;
        private Frame myFrame;
        private TextBlock pageHeader;
        public EncounterScreen()
        {
            this.InitializeComponent();
            esvm = EncounterScreenViewModel.Instance;
            DataContext = esvm;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Tuple<PokemonEntry, Frame,TextBlock> send = (Tuple<PokemonEntry, Frame,TextBlock>)e.Parameter;
            esvm.CurrentPokemon = send.Item1;
            myFrame = send.Item2;
            pageHeader = send.Item3;
        }

        private void Catch_Click(object sender, RoutedEventArgs e)
        {
            esvm.Catch();
            BackToMap();
        }

        private void Fled_Click(object sender, RoutedEventArgs e)
        {
            BackToMap();
        }

        private void BackToMap()
        {
            myFrame.Navigate(typeof(TownMapPage), pageHeader);
        }
    }
}
