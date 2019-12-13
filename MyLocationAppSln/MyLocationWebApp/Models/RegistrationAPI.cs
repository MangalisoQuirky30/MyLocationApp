
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyLocationApp.Models
{
    public class RegistrationAPI
    { 

        public string UserName;





        public string UserEmail;



        public string UserPhoneNumber;


        public string UserPassword;



        public string UserConfirmPassword;




        [Key]
        public int UserID { get; }
    }
}
