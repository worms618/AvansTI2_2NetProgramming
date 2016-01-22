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

namespace UwpProject.BillsPcPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BillsPcScreen : Page
    {
        public List<PokemonEntry> myPokemon
        {
            get { return (List<PokemonEntry>)GetValue(EntryProperty); }
            set { SetValue(EntryProperty, value); }
        }

        public static readonly DependencyProperty EntryProperty =
            DependencyProperty.Register("Entries", typeof(List<PokemonEntry>), typeof(BillsPcScreen), null);

        public BillsPcScreen()
        {
            this.InitializeComponent();
            myPokemon = new List<PokemonEntry>();
            myPokemon = BillsPc.Instance.MyPokemon;

            //File.WriteAllText("Storage/pokedexentries.json", json);
            DataContext = this;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "Pokédex";
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            PokemonEntry pokemonEntry = (PokemonEntry)e.ClickedItem;
            this.Frame.Navigate(typeof(PokemonEntryTemplate), pokemonEntry);
        }
    }
}
