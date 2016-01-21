using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UwpProject.Data
{
    public class PokemonEntry
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string EntryName { get; private set; }
        public string Weight { get; private set; }
        public string PokeHeight { get; private set; }
        public string ImageSource { get; private set; }
        public int HP { get; private set; }
        public int Attack{get; private set;}
        public int Defense{get; private set;}
        public int SpecialAttack{get; private set;}
        public int SpecialDefense{get; private set;}
        public int Speed{get; private set;}

        public PokemonEntry(int Id, string Name, string Mass, string PokeHeight, int hp, int atk, int def, int spatk, int spdef, int spd)
        {
            this.Id = Id;
            this.EntryName = "#" + Id + " " + Name;
            this.Name = Name;
            this.Weight = Mass + " lbs";
            this.PokeHeight = PokeHeight + "m";
            this.Attack = atk;
            this.Defense = def;
            this.SpecialAttack = spatk;
            this.SpecialDefense = spdef;
            this.Speed = spd;

            this.ImageSource = "ms-appx:///Assets/Sprites/" + this.Id + ".gif";
        }


        public override string ToString()
        {
            return EntryName;
        }
    }
}
