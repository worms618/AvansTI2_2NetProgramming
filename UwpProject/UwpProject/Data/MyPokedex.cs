using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UwpProject.Pokedex;
using Windows.UI.Xaml;

namespace UwpProject.Data
{
    public class MyPokedex
    {
        private static MyPokedex _instance;
        public static MyPokedex Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MyPokedex();
                }
                return _instance;
            }
        }

        private List<PokemonEntry> Entries;
        public List<PokemonEntry> Pokedex
        {
            get { return Entries; }
            set { Entries = value; }
        }
    }
}
