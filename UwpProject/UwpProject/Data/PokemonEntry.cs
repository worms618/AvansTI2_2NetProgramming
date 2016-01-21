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

        public PokemonEntry(int Id, string Name, string Weight, string PokeHeight, int HP, int Attack, int Defense, int SpecialAttack, int SpecialDefense, int Speed)
        {
            this.Id = Id;
            this.EntryName = "#" + Id + " " + Name;
            this.Name = Name;
            this.Weight = Weight;
            if (PokeHeight.Length < 3)
            {
                this.PokeHeight = "0." + PokeHeight;
            }
            else
            {
                this.PokeHeight = PokeHeight.Insert(PokeHeight.Length - 2, ".");
            }
            this.Attack = Attack *2;
            this.Defense = Defense *2;
            this.SpecialAttack = SpecialAttack *2;
            this.SpecialDefense = SpecialDefense *2;
            this.Speed = Speed *2;

            this.ImageSource = "ms-appx:///Assets/Sprites/" + this.Id + ".gif";
        }


        public override string ToString()
        {
            return EntryName;
        }
    }
}
