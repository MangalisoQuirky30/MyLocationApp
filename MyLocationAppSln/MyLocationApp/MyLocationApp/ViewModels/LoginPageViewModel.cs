using MyLocationApp.Messages;
using MyLocationApp.Models;
using MyLocationApp.Services;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Essentials;

namespace MyLocationApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;
        private IEventAggregator _eventAggregator;

        public Login userInfo { get; set; }
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IEventAggregator eventAggregator)
           : base(navigationService)
        {
            Title = "Sign Up";

            _pageDialogService = pageDialogService;
            _eventAggregator = eventAggregator;
            var regInfo = new Login();
            userInfo = regInfo;
        }

        private DelegateCommand _SignUpCommand;
        public DelegateCommand SignUpCommand =>
            _SignUpCommand ?? (_SignUpCommand = new DelegateCommand(ExecuteSignUpCommand));

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(ExecuteSignInCommand));

        async void ExecuteSignUpCommand()
        {

            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/RegisterPage", useModalNavigation: true);
        }

        async void ExecuteSignInCommand()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                CheckStatus.LoggedIn = true;
                _eventAggregator.GetEvent<LoginMessage>().Publish();
                // Connection to internet is available

                HttpClient client = new HttpClient();
                string allUsers = await client.GetStringAsync("http://10.0.2.2:5000/registration");
                var user = allUsers.Substring(1, 139);
                var everyone = JsonConvert.DeserializeObject<ObservableCollection<Registration>>(user);


                var reg = new Login()
                {
                    UserEmail = userInfo.UserEmail,
                    UserPassword = userInfo.UserPassword
                };
                var email = reg.userEmail;
                var pass = reg.UserPassword;


                for (var i = 0; i < everyone.Count; i++)
                {
                    if (email != null && pass != null)
                    {
                        if (everyone[i].UserEmail == email && everyone[i].UserPassword == pass)
                        {
                            await _pageDialogService.DisplayAlertAsync("Internet", "You have been successly signed in.", "Ok");
                            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/HomePage", useModalNavigation: true);
                        }
                        else
                        {
                            await _pageDialogService.DisplayAlertAsync("SIGN IN", "You have entered invalid credentails. Please try again.", "OK");
                        }
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("SIGN IN", "Please fill in missing blocks", "OK");
                    }
                }

               

               


            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Internet", "You do not have internet access. Use local data?", "Ok", "No Thanks");
            }

        }
    }
}