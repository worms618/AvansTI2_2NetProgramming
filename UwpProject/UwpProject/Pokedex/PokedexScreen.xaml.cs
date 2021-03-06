﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UwpProject.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
        public List<PokemonEntry> Entries
        {
            get { return (List<PokemonEntry>)GetValue(EntryProperty); }
            set { SetValue(EntryProperty, value); }
        }

        public static readonly DependencyProperty EntryProperty =
            DependencyProperty.Register("Entries", typeof(List<PokemonEntry>), typeof(PokedexScreen), null);

        public PokedexScreen()
        {
            this.InitializeComponent();
            Entries = new List<PokemonEntry>();
            Entries = BillsPc.Instance.Entries;
            
            //File.WriteAllText("Storage/pokedexentries.json", json);
            DataContext = this;
        }        

        //private async Task getPokemonInstance()
        //{
        //    for (int i = 1; i <= 151; i++)
        //    {
        //        PokeAPI.Pokemon p = await PokeAPI.Pokemon.GetInstanceAsync(i);
        //        Items.Add(p);
        //        Entries.Add(new PokemonEntry(p.Id,p.Name,p.Mass.ToString(),p.Height.ToString(),p.HP,p.Attack,p.Defense,p.SpecialAttack,p.SpecialDefense,p.Speed));
        //    }
        //    string json = JsonConvert.SerializeObject(Entries);

        //    Windows.Storage.StorageFolder storageFolder =   Windows.Storage.ApplicationData.Current.LocalFolder;
        //    Windows.Storage.StorageFile sampleFile =     await storageFolder.CreateFileAsync("pokedexentries.json",       Windows.Storage.CreationCollisionOption.ReplaceExisting);
        //    await Windows.Storage.FileIO.WriteTextAsync(sampleFile, json);
        //}

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
