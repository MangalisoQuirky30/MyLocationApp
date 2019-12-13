using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLocationApp.Models
{
    public class Registration : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Required]
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


        [PrimaryKey , AutoIncrement]
        public int UserID { get; }


        [Required]
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

        [Required]
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

        [Required]
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

        [Required]
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
    }
}
