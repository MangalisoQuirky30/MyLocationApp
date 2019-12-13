using MyLocationApp.Models;
using MyLocationApp.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms.Maps;
using MyLocationApp.Services;
using Xamarin.Essentials;
using Prism.Services;
using MyLocation = MyLocationApp.Models.MyLocation;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using Xamarin.Essentials;

namespace MyLocationApp.ViewModels
{
    public class MyLocationPageViewModel : BindableBase
    {

        private IDatabase _sqliteMethods;

        //private ControllerBase _myLocationAPIsController;

        private IPageDialogService _pageDialogService;


        readonly ObservableCollection<MyLocation> _locations;
        public ObservableCollection<Models.Location> PositionList;
        public IEnumerable PositionListItem => PositionList;
        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }


        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }


        private Position _position;
        public Position Position
        {
            get { return _position; }
            set { SetProperty(ref _position, value); }
        }


        private DelegateCommand _addLocationCommand;
        public DelegateCommand AddLocationCommand =>
            _addLocationCommand ?? (_addLocationCommand = new DelegateCommand(ExecuteAddLocation));
        public ObservableCollection<MyLocation> Locations => _locations;
        public MyLocationPageViewModel(INavigationService navigationService, IDatabase dBmapping, IPageDialogService pageDialogService) : base()
        {
            _locations = new ObservableCollection<MyLocation>();
            GetData();

            _locations.Add(new MyLocation()
            {
                Address = "Squeaky Bone",
                Description = "ghjkf",
                Latitude = -33.94374,
                Longitude = 18.5302,
            });

            _locations.Add(new MyLocation()
            {
                Address = "Squeaky Bone",
                Description = "ghjkf",
                Longitude = -33.94384,
                Latitude = 18.5309,
            });

            _sqliteMethods = dBmapping;

            _pageDialogService = pageDialogService;

        }

        SQLiteMethods sqlite { get; set; }


        private async void ExecuteAddLocation()
        {
            HttpClient client = new HttpClient();
            var url = "http://10.0.2.2:5000/location";


            try
            {

                var newLocation = await _sqliteMethods.GetCurrentLocation();
                _locations.Add(newLocation);

                await _sqliteMethods.SaveNewLocationAsync(newLocation);
                
                var json = JsonConvert.SerializeObject(_locations);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(url, content);

                await _pageDialogService.DisplayAlertAsync("Exception", response.ReasonPhrase, "Ok");
            }
            catch (FeatureNotEnabledException ex)
            {
                await _pageDialogService.DisplayAlertAsync("Exception", "Please turn on your GPS location", "Ok");
            }
        }


        /*  
            var client = new HttpClient();
            var url = "http://10.0.2.2:5000/registration";

            try
            {
                var json = JsonConvert.SerializeObject(reg);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                await _pageDialogService.DisplayAlertAsync("Happiness", result.ReasonPhrase, "Cool");*/

        public async void GetData()
        {
            var client = new HttpClient();
            var url = "http://10.0.2.2:5000/location";
            var stuff = await client.GetStringAsync(url);

            var split = stuff.Split('}');


            PositionList = new ObservableCollection<Models.Location>();

            for (var i = 0; i < split.Length - 1; i++)
            {
                var newSplit = split[i].Substring(1, split[i].Length - 1);
                newSplit = newSplit + "}";

                var lokasie = JsonConvert.DeserializeObject<MyLocation>(newSplit);
                _locations.Add(lokasie);

                PositionList.Add(new Models.Location(lokasie.Address, lokasie.Description, new Position(lokasie.Latitude, lokasie.Longitude)));
                Position = new Position(lokasie.Latitude, lokasie.Longitude);
                Address = lokasie.Address;
                //Description = lokasie.Description;





                //{
                /*Position = new Position(lokasie.Latitude, lokasie.Longitude),
                Address = lokasie.Address,
                Description = lokasie.Description,
                Safety = lokasie.Safety,
                Message = lokasie.Message*/

                //}) ;


            }
        }
    }
}