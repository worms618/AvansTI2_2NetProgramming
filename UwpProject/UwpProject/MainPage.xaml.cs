using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Encounter;
using UwpProject.Model;
using UwpProject.MyPokemon;
using UwpProject.Pokedex;
using UwpProject.Settings;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UwpProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(SplashScreenPage), PageHeader);
            var create = TownMapViewModel.Instance;
            WalkKeeper.EncounterTrigger += WalkKeeper_EncounterTrigger;     
        }

        private void WalkKeeper_EncounterTrigger(object sender, EventArgs e)
        {
            //Debug.WriteLine("Encounter");
            MyFrame.Navigate(typeof(EncounterScreen), PageHeader);
            _pokedexPage.IsSelected = false;
            _pokemonPage.IsSelected = false;
            _townmapPage.IsSelected = false; 
            _settingsPage.IsSelected = false;
        }

        private void MyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_pokedexPage.IsSelected)
            {
                MyFrame.Navigate(typeof(PokedexScreen), PageHeader);                
            }
            else if (_pokemonPage.IsSelected)
            {
                MyFrame.Navigate(typeof(MyPokemonPage), PageHeader);
            }
            else if (_townmapPage.IsSelected)
            {
                MyFrame.Navigate(typeof(TownMapPage), PageHeader);
            }
            else if (_settingsPage.IsSelected)
            {
                MyFrame.Navigate(typeof(SettingsPage), PageHeader);
            }
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        ///<summary>
        /// two methods to navigate through the backstack.
        /// </summary>

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoForward)
            {
                MyFrame.GoForward();
            }
        }
    }
}
