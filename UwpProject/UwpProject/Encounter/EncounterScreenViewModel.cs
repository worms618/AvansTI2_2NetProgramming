﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UwpProject.Data;
using UwpProject.Model;

namespace UwpProject.Encounter 
{
    public class EncounterScreenViewModel : INotifyPropertyChanged
    {
        private static EncounterScreenViewModel _instance;
        public static EncounterScreenViewModel Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EncounterScreenViewModel();
                }
                return _instance;
            }
        }        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyname)
        {
            var eventhandler = PropertyChanged;

            if (eventhandler != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Property is verandert: {propertyname}");
                eventhandler(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        
        private PokemonEntry _currentPokemon;
        public PokemonEntry CurrentPokemon
        {
            get { return _currentPokemon; }
            set { _currentPokemon = value; OnPropertyChanged(nameof(CurrentPokemon)); }
        }

        private EncounterScreenViewModel()
        {
            
        }

        public void Catch()
        {
            BillsPc.Instance.MyPokemon.Add(CurrentPokemon);
            Debug.WriteLine($"{BillsPc.Instance.MyPokemon.Count}");
        }
    }
}
