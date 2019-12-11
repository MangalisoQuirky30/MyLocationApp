using MyLocationApp.Models;
using MyLocationApp.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;
using MyLocationApp.Models;
using Prism.Services;

namespace MyLocationApp.Services
{
    class SQLiteMethods : IDatabase
    {
        private IPageDialogService _pageDialogService;

        static SQLiteAsyncConnection database;
        public  SQLiteMethods()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocationsSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Registration>().Wait();
            //database.CreateTableAsync<MyLocation>().Wait();
        }

        public static SQLiteAsyncConnection Database
        {
            get
            {
                if (database == null)
                {
                    database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EducateUsersSQLite.db3"));
                }
                return database;
            }
        }

        public async Task<int> SaveItemAsync(Registration user)
        {
            if (user.UserID != 0)
            {
                return await database.UpdateAsync(user);
            }
            else
            {
                return await database.InsertAsync(user);
            }
        }


        public async Task SaveNewLocationAsync(MyLocation location)
        {
            await database.InsertAsync(location);
        }


        public async Task<Registration> GetItemAsync(int id)
        {
            return await database.Table<Registration>().Where(i => i.UserID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Registration>> GetItemsAsync()
        {
            return await database.Table<Registration>().ToListAsync();
        }

        public async Task<int> DeleteItemAsync(Registration user)
        {
            return await database.DeleteAsync(user);
        }

        // int _pinCreatedCount = 0;


        

        async Task<MyLocation> IDatabase.GetCurrentLocation()
        {
            var noLocation = new MyLocation();

            try { 
           
                var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();

                var pins = await Geocoding.GetPlacemarksAsync(loc.Latitude, loc.Longitude);

                var firstPin = pins.FirstOrDefault();

                var myLocation = new MyLocation()
                {
                Address = firstPin.SubLocality,
                Description = firstPin.FeatureName,
                Position = new Xamarin.Forms.Maps.Position(loc.Latitude, loc.Longitude)
                };

                return myLocation;
            }
            catch (FeatureNotEnabledException ex)
            {
                await _pageDialogService.DisplayAlertAsync("Exception", "Please turn on your GPS location", "Ok");
                return noLocation;
            }

    //return new MyLocation(firstPin.SubLocality, firstPin.FeatureName, new Xamarin.Forms.Maps.Position(loc.Latitude, loc.Longitude));

}
    }
}


