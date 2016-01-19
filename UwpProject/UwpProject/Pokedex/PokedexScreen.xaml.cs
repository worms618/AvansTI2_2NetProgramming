using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

namespace UwpProject.Pokedex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PokedexScreen : Page
    {
        ObservableCollection<PokeAPI.Pokemon> Items;

        public PokedexScreen()
        {
            this.InitializeComponent();

            getPokemonList();
        }
        
        public ObservableCollection<PokeAPI.Pokemon> getPokemonList()
        {
            if(Items == null)
            {
                Items = new ObservableCollection<PokeAPI.Pokemon>();
            }
            if(Items.Count == 0)
            {
                var task = getPokemonInstance();
            }
            return Items;
        }

        private async Task getPokemonInstance()
        {
            for (int i = 1; i < 152; i++)
            {
                PokeAPI.Pokemon p = await PokeAPI.Pokemon.GetInstanceAsync(i);
                Items.Add(p);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var value = (TextBlock)e.Parameter;
            value.Text = "Pokédex";
        }
    }
}
