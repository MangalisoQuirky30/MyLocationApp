using MyLocationApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyLocationApp.Services.Interfaces
{
    public interface IDatabase
    {
        Task<int> SaveItemAsync(Registration user);

        Task<Registration> GetItemAsync(int id);

        Task<List<Registration>> GetItemsAsync();
        Task<int> DeleteItemAsync(Registration user);

        Task SaveNewLocationAsync(MyLocation location);

        Task<Models.MyLocation> GetCurrentLocation();


    }
}
