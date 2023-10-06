using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPR6312_POE_Web_Application
{
    //Class to keep track of what user is logged in (see How To Display User Name in Asp.net MVC 2019, 2019)
    public class DisplayUsername
    {
        //Static variable to store the username (see How To Display User Name in Asp.net MVC 2019, 2019)
        public static string passUsername;

        //Method to set the username
        public static void getUserName(string username)
        {
            //Assigning the provided username to the passUsername variable (see How To Display User Name in Asp.net MVC 2019, 2019)
            passUsername = username;
        }
    }

}
