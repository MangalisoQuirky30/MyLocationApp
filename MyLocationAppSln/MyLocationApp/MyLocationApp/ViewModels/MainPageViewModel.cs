using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyLocationApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public readonly INavigationService NavigationService;


        private DelegateCommand _registerCommand;
        public DelegateCommand RegisterCommand =>
            _registerCommand ?? (_registerCommand = new DelegateCommand(ExecuteRegisterCommand));

        async void ExecuteRegisterCommand()
        {
            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/MainPage/RegisterPage", useModalNavigation: true);
        }
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand));


        async void ExecuteLoginCommand()
        {
            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/MainPage/LoginPage", useModalNavigation: true);

        }
        public MainPageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

    }
}
