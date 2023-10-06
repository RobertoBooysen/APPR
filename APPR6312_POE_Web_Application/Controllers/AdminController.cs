using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class AdminController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        public IActionResult AdminHome()
        {
            return View();
        }
        //Action method for displaying the list of admins (Troeslen & Japikse, 2021)
        public IActionResult AdminView()
        {
            List<TblAdmin> temp = Poe.TblAdmins.ToList();
            return View(temp);
        }

        //Method to hash or encrypt the password in the database (Chand, 2020)
        public string Hash_Password(string input)
        {
            //Creating a SHA256 (Chand, 2020)  
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //ComputeHash - returns byte array (Chand, 2020)
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                //Converting byte array to a string (Chand, 2020)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        //Action method for displaying the admin registration form (Troeslen & Japikse, 2021)
        public IActionResult AdminRegister()
        {
            return View();
        }

        //POST action method for handling admin registration form submission (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AdminRegister(TblAdmin user)
        {
            try
            {
                //Checking if the username or password fields are empty (Troeslen & Japikse, 2021)
                if (user.Password == null || user.Username == null)
                {
                    ViewBag.Error = "Please enter all fields";
                    return View();
                }
                else
                {
                    //Hashing the admins password before storing it in the database (Chand, 2020)
                    TblAdmin m = new TblAdmin
                    {
                        Username = user.Username,
                        Password = Hash_Password(user.Password)
                    };
                    //Adding the admin record to the database and save changes (Troeslen & Japikse, 2021)
                    Poe.TblAdmins.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the AdminView action (Troeslen & Japikse, 2021)
                    return RedirectToAction("AdminView");
                }
            }
            catch
            {
                ViewBag.Error = "Admin already in the database";
                return View();
            }
        }

        //Action method for displaying the admin login form (Troeslen & Japikse, 2021)
        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        //POST action method for handling admin login form submission (Troeslen & Japikse, 2021)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminLogin(TblAdmin admin)
        {
            try
            {
                //Checking if the provided username and password match a record in the database (Troeslen & Japikse, 2021)
                var detail = Poe.TblAdmins.Where(ur => ur.Username.Equals(admin.Username)
                     && ur.Password.Equals(Hash_Password(admin.Password))).FirstOrDefault();

                //Storing the logged-in username using DisplayUsername class (Troeslen & Japikse, 2021)
                DisplayUsername.getUserName(admin.Username);

                //Checking if the username or password fields are filled in correctly (Troeslen & Japikse, 2021)
                if (detail != null)
                {
                    //Redirecting to the AdminHome action if login is successful (Troeslen & Japikse, 2021)
                    return RedirectToAction("AdminHome", "Admin");
                }
                else
                {
                    ViewBag.Error = "Details don't match";
                }
            }
            catch
            {
                ViewBag.Error = "Admin already in the database";
            }
            return View();
        }
    }
}
