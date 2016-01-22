using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpProject.Pokedex;
using Windows.UI.Xaml;

namespace UwpProject.Data
{
    public class BillsPc
    {
        private static BillsPc _instance;
        public static BillsPc Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BillsPc();
                }
                return _instance;
            }
        }

        private List<PokemonEntry> _pokemonEntries;
        public List<PokemonEntry> Entries
        {
            get { return _pokemonEntries; }
            set { _pokemonEntries = value; }
        }

        private List<PokemonEntry> _myPokemon;
        public List<PokemonEntry> MyPokemon
        {
            get { return _myPokemon; }
            set { _myPokemon = value; }
        }

        private BillsPc()
        {
            Entries = new List<PokemonEntry>();
            MyPokemon = new List<PokemonEntry>();
            getPokemonList();            
        }

        private void getPokemonList()
        {
            string json = File.ReadAllText("Storage/pokedexentries.json");

            JObject JsonObject = JObject.Parse(json);
            IList<JToken> JsonList = JsonObject["Entries"].ToList();
            foreach (JToken pokemonentry in JsonList)
            {
                Entries.Add(JsonConvert.DeserializeObject<PokemonEntry>(pokemonentry.ToString()));
            }
            //System.Diagnostics.Debug.WriteLine(Entries.Count);
        }

    }
}
