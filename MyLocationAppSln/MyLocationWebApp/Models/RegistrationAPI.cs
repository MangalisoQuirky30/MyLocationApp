
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyLocationApp.Models
{
    public class RegistrationAPI : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }




        public string userEmail;
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                OnPropertyChanged("UserEmail");
            }
        }


        public string userPhoneNumber;
        public string UserPhoneNumber
        {
            get { return userPhoneNumber; }
            set
            {
                userPhoneNumber = value;
                OnPropertyChanged("UserPhoneNumber");
            }
        }


        public string userPassword;
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                OnPropertyChanged("UserPassword");
            }
        }


        public string userConfirmPassword;
        public string UserConfirmPassword
        {
            get { return userConfirmPassword; }
            set
            {
                userConfirmPassword = value;
                OnPropertyChanged("UserConfirmPassword");
            }
        }



        [System.ComponentModel.DataAnnotations.Key]
        public int UserID { get; }
    }
}
