using MyLocationApp.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using Prism.Services;
using System.Text;

namespace MyLocationApp.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public Registration userInfo { get; set; }


        private IPageDialogService _pageDialogService;

        readonly ObservableCollection<Registration> _allUsers;
        public readonly INavigationService NavigationService;
        public RegisterPageViewModel(INavigationService navigationService , IPageDialogService pageDialogService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            NavigationService = navigationService;
            var regInfo = new Registration();
            userInfo = regInfo;
        }


        private DelegateCommand _navSignInCommand;
        public DelegateCommand NavSignInCommand =>
            _navSignInCommand ?? (_navSignInCommand = new DelegateCommand(ExecuteNavSignInCommand));


        private DelegateCommand _signUpCommand;
        public DelegateCommand SignUpCommand =>
            _signUpCommand ?? (_signUpCommand = new DelegateCommand(ExecuteSignUpCommand));

        async void ExecuteSignUpCommand()
        {

            var reg = new Registration() { 
            
                UserName = userInfo.UserName,
                UserEmail = userInfo.UserEmail ,
                userPhoneNumber = userInfo.userPhoneNumber,
                UserPassword = userInfo.UserPassword,
                UserConfirmPassword = userInfo.UserConfirmPassword
            };



            var client = new HttpClient();
            var url = "http://10.0.2.2:5000/registration";

            try
            {
                var json = JsonConvert.SerializeObject(reg);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(url, content);
                await _pageDialogService.DisplayAlertAsync("Happiness", result.ReasonPhrase, "Cool");
            }
            catch (Exception ex)
            {
                var mess = ex.Message;
            }


            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/LoginPage", useModalNavigation: true);
        }

        async void ExecuteNavSignInCommand()
        {


            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/LoginPage", useModalNavigation: true);
        }
    }
}
