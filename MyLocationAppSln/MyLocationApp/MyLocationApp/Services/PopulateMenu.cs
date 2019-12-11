using MyLocationApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyLocationApp.Services
{
    public class PopulateMenu
    {
        public  void populateMenu(ObservableCollection<MenuItem> myList, bool status)
        {

            if(status == true)
            {
                myList.Add(new MenuItem()
                {
                    MenuItemName = "Map" ,
                    MenuItemImgSrc = "map.png" ,
                    MenuItemNavigation = "MasterMainPage/NavigationPage/HomePage"
                });
                myList.Add(new MenuItem()
                {
                    MenuItemName = "My Location",
                    MenuItemImgSrc = "mylocation.png",
                    MenuItemNavigation = "MasterMainPage/NavigationPage/MyLocationPage"
                });
                myList.Add(new MenuItem()
                {
                    MenuItemName = "Location History",
                    MenuItemImgSrc = "history.png",
                    MenuItemNavigation = "MasterMainPage/NavigationPage/HistoryPage"
                });
                myList.Add(new MenuItem()
                {
                    MenuItemName = "Sign Out",
                    MenuItemImgSrc = "signout.png",
                    MenuItemNavigation = "MasterMainPage/NavigationPage/MainPage"
                });
            } else
            {
                myList.Add(new MenuItem()
                {
                    MenuItemName = "Sign In",
                    MenuItemImgSrc = "signin.png",
                    MenuItemNavigation = "MasterMainPage/NavigationPage/LoginPage"
                });
                myList.Add(new MenuItem()
                {
                    MenuItemName = "Sign Up",
                    MenuItemImgSrc = "signup.png",
                    MenuItemNavigation = "MasterMainPage/NavigationPage/RegisterPage"
                });
            }
        }
    }
}
