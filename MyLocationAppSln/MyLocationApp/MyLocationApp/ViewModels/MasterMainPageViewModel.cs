using MyLocationApp.Messages;
using MyLocationApp.Models;
using MyLocationApp.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyLocationApp.ViewModels
{
    public class MasterMainPageViewModel : BindableBase
    {
        public readonly INavigationService NavigationService;

        private IEventAggregator _eventAggregator;


        private ObservableCollection<MenuItem> _menuList;
        public ObservableCollection<MenuItem> MenuList
        {
            get { return _menuList; }
            set { SetProperty(ref _menuList, value); }  
        }

        public MasterMainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            NavigationService = navigationService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<LoginMessage>().Subscribe(Login);

            try
            {
                var status = CheckStatus.CheckLoginStatus();
                //  MenuList = new ObservableCollection<MenuItem>();
                MenuList = new ObservableCollection<MenuItem>();


                var stuff = new PopulateMenu();
                stuff.populateMenu(MenuList, status);
            }
            catch (Exception ex)
            {
                //sdfghjklkjhgfd
            }
        }



           



        // public IObservable<>

        private DelegateCommand<MenuItem> _navigateCommand;
        public DelegateCommand<MenuItem> NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand<MenuItem>(ExecuteNavigateCommand));

        private async void ExecuteNavigateCommand(MenuItem menuItem)
        {
            await NavigationService.NavigateAsync(menuItem.MenuItemNavigation);
        }

        private IList<MenuItem> _menuItems;
        public IList<MenuItem> Menuitems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

       // BindingContext = this


        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(ExecuteRegisterCommand));

        async void ExecuteRegisterCommand()
        {
            await NavigationService.NavigateAsync("RegisterPage");
        }
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));


        async void ExecuteLoginCommand()
        {
            await NavigationService.NavigateAsync("LoginPage");

        }

        private void Login()
        {
            try
            {
                var status = CheckStatus.CheckLoginStatus();
                MenuList = new ObservableCollection<MenuItem>();

                var stuff = new PopulateMenu();
                stuff.populateMenu(MenuList, status);
            }
            catch (Exception ex)
            {
                //sdfghjklkjhgfd
            }
        }
    
    }
}
