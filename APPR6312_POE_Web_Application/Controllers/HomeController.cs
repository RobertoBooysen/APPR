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
            //Connection to the database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();

            //Retrieving the count of records in the TblGoodsDonations table (Troeslen & Japikse, 2021)
            int donationCount = Poe.TblGoodsDonations.Count();

            //Initialize totalAllocatedItems to 0 (Troeslen & Japikse, 2021)
            int totalAllocatedItems = 0;

            //Iterating through each row in Poe.TblDisasters (Troeslen & Japikse, 2021)
            foreach (var row in Poe.TblDisasters)
            {
                //Initializing rowtotalAllocatedItems for the current row to 0 (Troeslen & Japikse, 2021)
                int rowtotalAllocatedItems = row.AllocatedGoods
                    .Split(',') //Splitting the AllocatedGoods string into parts using a comma as the delimiter (Troeslen & Japikse, 2021)
                    .Select(part => int.TryParse(part.Split(':').Last().Trim(), out int value) ? value : 0)
                    //Attempting to parse the numeric value after the colon; if successful, return the parsed value, otherwise return 0 (Troeslen & Japikse, 2021)
                    .Sum(); //Calculating the sum of rowtotalAllocatedItems (Troeslen & Japikse, 2021)

                //Adding rowtotalAllocatedItems to totalAllocatedItems (Troeslen & Japikse, 2021)
                totalAllocatedItems += rowtotalAllocatedItems;
            }

            //Storing the grand total sum in ViewData with the key "AllocatedGoodsSum" (Troeslen & Japikse, 2021)
            ViewData["AllocatedGoodsSum"] = totalAllocatedItems;


            //Retrieving the sum in ViewData in the TblMonetaryDonations table (Troeslen & Japikse, 2021)
            var totalAmount = Poe.TblMonetaryDonations.Sum(donation => donation.Amount);

            //Storing the monetary donation sum amount in ViewData (Troeslen & Japikse, 2021)
            ViewData["MonetaryAmount"] = totalAmount;

            //Retrieving the count of active disasters in the TblDisasters table (Troeslen & Japikse, 2021)
            int activeDisasterCount = Poe.TblDisasters.Count(disaster => disaster.Status == "Active");

            //Storing the active disaster count in ViewData (Troeslen & Japikse, 2021)
            ViewData["ActiveDisastersCount"] = activeDisasterCount;

            //Retrieving a list of active disasters
            List<TblDisaster> activeDisasters = Poe.TblDisasters
                .Where(d => d.Status == "Active")
                .ToList();

            //Creating a list to store the allocated goods for each active disaster (Troeslen & Japikse, 2021)
            List<string> allocatedGoodsList = new List<string>();

            //Retrieving allocated goods for each active disaster (Troeslen & Japikse, 2021)
            foreach (var disaster in activeDisasters)
            {
                allocatedGoodsList.Add(disaster.AllocatedGoods);
            }

            //Getting the total number of items in TblGoodsDonations (Troeslen & Japikse, 2021)
            int numberOfItems = Poe.TblGoodsDonations.Count();

            //Calculating the total allocated money for active disasters (Troeslen & Japikse, 2021)
            int allocateMoney = Poe.TblDisasters.Sum(d => d.AllocatedMoney);

            //Set ViewBag properties to pass data to the view (Troeslen & Japikse, 2021)
            ViewBag.TotalAmount = totalAmount;
            ViewBag.NumberOfItems = numberOfItems;
            ViewBag.AllocateMoney = allocateMoney;
            ViewBag.CurrentActiveDisasters = activeDisasters;
            ViewBag.AllocatedGoodsList = allocatedGoodsList;

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
