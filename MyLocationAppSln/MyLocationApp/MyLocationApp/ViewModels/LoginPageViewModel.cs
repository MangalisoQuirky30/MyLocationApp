using MyLocationApp.Messages;
using MyLocationApp.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace MyLocationApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;
        private IEventAggregator _eventAggregator;

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEventAggregator eventAggregator)
           : base(navigationService)
        {
            Title = "Sign Up";

            _pageDialogService = pageDialogService;
            _eventAggregator = eventAggregator;
        }

        private DelegateCommand _SignUpCommand;
        public DelegateCommand SignUpCommand =>
            _SignUpCommand ?? (_SignUpCommand = new DelegateCommand(ExecuteSignUpCommand));

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand));

        async void ExecuteSignUpCommand()
        {

            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/RegisterPage");
        }

        async void ExecuteSignInCommand()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                CheckStatus.LoggedIn = true;
                _eventAggregator.GetEvent<LoginMessage>().Publish();
                await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/HomePage");
                // Connection to internet is available
                await _pageDialogService.DisplayAlertAsync("Internet", "Awesome, you have internet access", "Ok");
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Internet", "You do not have internet access. Use local data?", "Ok", "No Thanks");
            }

        }
    }
}