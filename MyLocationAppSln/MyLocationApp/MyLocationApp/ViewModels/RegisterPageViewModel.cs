using MyLocationApp.Models;
using MyLocationApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyLocationApp.Services;

namespace MyLocationApp.ViewModels
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public Registration userInfo { get; set; }


        readonly ObservableCollection<Registration> _allUsers;
        public readonly INavigationService NavigationService;
        public RegisterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
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

            // var sqlite = new SQLiteMethods();

            var database = SQLiteMethods.Database;
            await database.InsertAsync(reg);
                //SaveItemAsync(reg);

             await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/LoginPage");
        }

        async void ExecuteNavSignInCommand()
        {


            await NavigationService.NavigateAsync("MasterMainPage/NavigationPage/LoginPage");
        }
    }
}
