using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UwpProject.Data;
using UwpProject.Pokedex;
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

namespace UwpProject.MyPokemon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MyPokemonPage : Page
    {
        PokedexScreen ps = new PokedexScreen();

        public MyPokemonPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "My Pokémon";
        }

        private void Pokemon1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[0];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[55];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[22];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[87];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[146];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = ps.Entries[97];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }
    }
}
