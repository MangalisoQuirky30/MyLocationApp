using MyLocationApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MyLocationApp.Services;
using MyLocationApp.Services.Interfaces;
using Prism.Services;
using Prism.Navigation;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;

namespace MyLocationApp.ViewModels
{
    public class MyLocationPageViewModel : BindableBase
    {

        private IDatabase _sqliteMethods;

        private IPageDialogService _pageDialogService;

        readonly ObservableCollection<MyLocation> _locations;

        public ObservableCollection<MyLocation> Locations => _locations;
        public MyLocationPageViewModel(INavigationService navigationService, IDatabase dBmapping, IPageDialogService pageDialogService) : base()
        {
            _locations = new ObservableCollection<MyLocation>();

            _locations.Add(new MyLocation()
            {
                Address = "Langa",
                Description = "Home",
                Position = new Position(-33.94374, 18.5302)
            });

            _locations.Add(new MyLocation()
            {
                Address = "Nandos",
                Description = "That chicken plek",
                Position = new Position(-33.933533, 18.407378)
            }) ;

            _sqliteMethods = dBmapping;

            _pageDialogService = pageDialogService;

        }


        private DelegateCommand _addLocationCommand;
        public DelegateCommand AddLocationCommand =>
            _addLocationCommand ?? (_addLocationCommand = new DelegateCommand(ExecuteAddLocation));

        void ExecuteAddLocationCommand()
        {

        }


        private async void ExecuteAddLocation()
        {
            try
            {

                var newLocation = await _sqliteMethods.GetCurrentLocation();
                _locations.Add(newLocation);

               // await _sqliteMethods.SaveNewLocationAsync(newLocation);

                /*
                await PostMyLocationAPI(newLocation);*/
                HttpClient client = new HttpClient();
                var json = JsonConvert.SerializeObject(newLocation); 
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44346/MyLocationAPIs", content);
                await _pageDialogService.DisplayAlertAsync("Exception", response.ReasonPhrase, "cool");
                
            }
            catch (FeatureNotEnabledException ex)
            {
                await _pageDialogService.DisplayAlertAsync("Exception", "Please turn on your GPS location", "Ok");
            }
        }

    }
}
