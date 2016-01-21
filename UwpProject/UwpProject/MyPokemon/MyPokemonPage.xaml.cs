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
using Windows.UI.Xaml.Media.Imaging;
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

        List<PokemonEntry> list = new List<PokemonEntry>();

        public MyPokemonPage()
        {
            this.InitializeComponent();
            FillList();
            Namen();
            Plaatjes();
        }

        public void FillList()
        {
            list.Add(ps.Entries[0]);
            list.Add(ps.Entries[55]);
            list.Add(ps.Entries[22]);
            list.Add(ps.Entries[87]);
            list.Add(ps.Entries[146]);
            list.Add(ps.Entries[97]);
        }

        public void Namen()
        {
            NaamPokemon1.Text = list[0].Name;
            NaamPokemon2.Text = list[1].Name;
            NaamPokemon3.Text = list[2].Name;
            NaamPokemon4.Text = list[3].Name;
            NaamPokemon5.Text = list[4].Name;
            NaamPokemon6.Text = list[5].Name;
        }

        public void Plaatjes()
        {
            BitmapImage img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[0].Id + ".gif");
            Pokemon1.Source = img;

            img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[1].Id + ".gif");
            Pokemon2.Source = img;

            img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[2].Id + ".gif");
            Pokemon3.Source = img;

            img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[3].Id + ".gif");
            Pokemon4.Source = img;

            img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[4].Id + ".gif");
            Pokemon5.Source = img;

            img = ImageFromRelativePath(this, "ms-appx:///Assets/Sprites/" + list[5].Id + ".gif");
            Pokemon6.Source = img;
        }

        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = uri;
            return bmp;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "My Pokémon";
        }

        private void Pokemon1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[0];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[1];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[2];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[3];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[4];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }

        private void Pokemon6_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PokemonEntry pokemonEntry = list[5];
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }
    }
}
