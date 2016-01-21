﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using UwpProject.Model;
using UwpProject.Model.Places;
using Windows.Devices.Geolocation;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Xaml.Controls.Maps;

namespace UwpProject.TownMap
{
    public class TownMapViewModel
    {
        private static TownMapViewModel _instance;
        public static TownMapViewModel Instance
        {
            get {
                    if(_instance == null)
                    {
                        _instance = new TownMapViewModel();
                    }
                    return _instance;
                }            
        }

        private Geolocator _locator;

        private Geopoint _currentLocation;
        public Geopoint CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; LocationIcon.Location = value; }
        }

        public MapIcon LocationIcon { get; }

        private WalkKeeper walker;

        public List<SpecialPlace> SpecialPlaces { get; }

        private TownMapViewModel()
        {
            LocationIcon = MapElementFactory.MakeMapIcon(null, new Uri("ms-appx:///Assets/pokebal_16x16.png"), 1);
            walker = new WalkKeeper();
            walker.EncounterTrigger += Walker_EncounterTrigger;
            SpecialPlaces = new List<SpecialPlace>();
            SpecialPlaces.Add(new PokeCenter("Pokecenter",new BasicGeoposition { Latitude = 51.806380 , Longitude = 4.896346 }));            
            SetUpGeolocator();                        
        }

        private async void SetUpGeolocator()
        {
            var response = await Geolocator.RequestAccessAsync();
            switch (response)
            {
                case GeolocationAccessStatus.Allowed:
                    //Debug.WriteLine("acces allowed");
                    _locator = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                    SetCurrentLocation();
                    break;
                case GeolocationAccessStatus.Denied:
                    Debug.WriteLine("Acces denied");
                    _locator = null;
                    break;
                case GeolocationAccessStatus.Unspecified:
                    Debug.WriteLine("Acees unspecified");
                    _locator = null;
                    break;
            }
        }
        
        public async void SetCurrentLocation()
        {
            if(_locator != null)
            {
                var position = await _locator.GetGeopositionAsync();
                if (position != null)
                {
                    if(CurrentLocation != null)
                    {
                        walker.Walk(CurrentLocation.Position, position.Coordinate.Point.Position);
                    }                    
                    CurrentLocation = position.Coordinate.Point;                   
                    //Debug.WriteLine($"Currentlocation Latitude: {CurrentLocation.Position.Latitude},Longitude: {CurrentLocation.Position.Longitude}");
                    //Debug.WriteLine($"Mapicon Latitude: {LocationIcon.Location.Position.Latitude},Longitude: {LocationIcon.Location.Position.Longitude}");                    
                }
            }            
        }        

        private void Walker_EncounterTrigger(object sender, EventArgs e)
        {
            Debug.WriteLine($"Encouter is gekomen!");            
        }        
    }
}