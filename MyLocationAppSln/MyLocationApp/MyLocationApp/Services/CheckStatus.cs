using System;
using System.Collections.Generic;
using System.Text;

namespace MyLocationApp.Services
{
    class CheckStatus
    {
        public static bool LoggedIn { get; set; }

        public static bool CheckLoginStatus()
        {
            return LoggedIn;
        }
    }
}
