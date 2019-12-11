using MyLocationApp.Models;
using MyLocationApp.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using MyLocationApp.Services;
using Xamarin.Essentials;
using Prism.Services;
using MyLocation = MyLocationApp.Models.MyLocation;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyLocationApp.ViewModels
{
    public class HomePageViewModel : BindableBase
    {

        private IDatabase _sqliteMethods;

        //private ControllerBase _myLocationAPIsController;

        private IPageDialogService _pageDialogService;

        readonly ObservableCollection<MyLocation> _locations;

        public ObservableCollection<MyLocation> Locations => _locations;
        public HomePageViewModel(INavigationService navigationService, IDatabase dBmapping, IPageDialogService pageDialogService) : base()
        {
            _locations = new ObservableCollection<MyLocation>();
            /*
            {
                new MyLocation("Langa", "Home", new Position(-33.94374, 18.5302)),
                new MyLocation("Nandos", "Cause why not?", new Position(-33.933533,  18.407378))



            };
            */

            _locations.Add(new MyLocation()
            {
                Address = "Langa" ,
                Description = "Home",
                Position = new Position(-33.94374, 18.5302) 
            });

            _locations.Add(new MyLocation()
            {
                Address = "Nandos",
                Description = "That chicken plek" ,
                Position = new Position(-33.933533, 18.407378)
            });

            _sqliteMethods = dBmapping;

            _pageDialogService = pageDialogService;

        }

        SQLiteMethods sqlite { get; set; }


        private DelegateCommand _addLocationCommand;
        public DelegateCommand AddLocationCommand =>
            _addLocationCommand ?? (_addLocationCommand = new DelegateCommand(ExecuteAddLocation));

        private DelegateCommand _removeLocationCommand;
        public DelegateCommand RemoveLocationCommand =>
            _removeLocationCommand ?? (_removeLocationCommand = new DelegateCommand(ExecuteRemoveLocation));


        private DelegateCommand _clearLocationCommand;
        public DelegateCommand ClearLocationCommand =>
            _clearLocationCommand ?? (_clearLocationCommand = new DelegateCommand(ExecuteClearLocation));


        private DelegateCommand _updateLocationCommand;
        public DelegateCommand UpdateLocationCommand =>
            _updateLocationCommand ?? (_updateLocationCommand = new DelegateCommand(ExecuteUpdateLocation));


        private DelegateCommand _replaceLocationCommand;
        public DelegateCommand ReplaceLocationCommand =>
            _replaceLocationCommand ?? (_replaceLocationCommand = new DelegateCommand(ExecuteReplaceLocation));

        private async void ExecuteAddLocation()
        {
            try {

                var newLocation = await _sqliteMethods.GetCurrentLocation();
                _locations.Add(newLocation);
                
                await _sqliteMethods.SaveNewLocationAsync(newLocation);

                //await PostMyLocationAPI(newLocation);
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(_locations);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44346/MyLocationAPIs", content);

                await _pageDialogService.DisplayAlertAsync("Exception", response.ReasonPhrase, "Ok");
            }
            catch (FeatureNotEnabledException ex)
            {
                await _pageDialogService.DisplayAlertAsync("Exception", "Please turn on your GPS location", "Ok");
            }
        }

        private void ExecuteRemoveLocation()
        {
        }


        private void ExecuteClearLocation()
        {
        }


        private void ExecuteUpdateLocation()
        {
            if (!_locations.Any())
            {
                return;
            }

            double lastLatitude = _locations.Last().Position.Latitude;
            foreach (MyLocation location in Locations)
            {
                location.Position = new Position(lastLatitude, location.Position.Longitude);
            }
        }


        private async void ExecuteReplaceLocation()
        {
            if (!_locations.Any())
            {
                return;
            }

            _locations[_locations.Count - 1] = await _sqliteMethods.GetCurrentLocation();

        }


    }


}