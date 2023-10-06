using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //This action method returns the "Index" view (Troeslen & Japikse, 2021)
        public IActionResult Index()
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();

            //Retrieving the count of records in the TblGoodsDonations table (Troeslen & Japikse, 2021)
            int donationCount = Poe.TblGoodsDonations.Count();

            //Storing the donation count in ViewData to pass it to the view (Troeslen & Japikse, 2021)
            ViewData["DonationCount"] = donationCount;

            //Retrieving the count of records in the TblMonetaryDonations table (Troeslen & Japikse, 2021)
            int monetaryCount = Poe.TblMonetaryDonations.Count();

            //Storing the monetary donation count in ViewData (Troeslen & Japikse, 2021)
            ViewData["MonetaryCount"] = monetaryCount;

            //Retrieving the count of records in the TblDisasters table (Troeslen & Japikse, 2021)
            int disasterCount = Poe.TblDisasters.Count();

            //Storing the disaster count in ViewData (Troeslen & Japikse, 2021)
            ViewData["DisastersCount"] = disasterCount;

            //Return the "Index" view along with ViewData (Troeslen & Japikse, 2021)
            return View();
        }

        //This action method returns the "AboutUs" view (Troeslen & Japikse, 2021)
        public IActionResult AboutUs()
        {
            return View();
        }

        //This action method returns the "ContactUs" view (Troeslen & Japikse, 2021)
        public IActionResult ContactUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
