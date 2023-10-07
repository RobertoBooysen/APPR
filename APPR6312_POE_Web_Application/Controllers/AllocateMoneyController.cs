using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class AllocateMoneyController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to allocate money (Troeslen & Japikse, 2021)
        public IActionResult AddMoney()
        {
            var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
            ViewBag.ActiveDisasters = activeDisasters;
            return View();
        }

        //POST action method for handling the submission of a new monetary allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddMoney(TblAllocateMoney money)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (money.Disasters == null || money.Amount == 0)
                {
                    ViewBag.Error = "Please enter all fields";
                    var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                    ViewBag.ActiveDisasters = activeDisasters;
                    return View();
                }
                else
                {
                    //Refreshing the ViewBag.ActiveDisasters (Troeslen & Japikse, 2021)
                    var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                    ViewBag.ActiveDisasters = activeDisasters;

                    //Getting the last TotalReceived value from the last donation (Troeslen & Japikse, 2021)
                    var lastTotalReceived = Poe.TblMonetaryDonations
                        .Where(d => d.Username == DisplayUsername.passUsername)
                        .OrderByDescending(d => d.Date)
                        .Select(d => d.TotalReceived)
                        .FirstOrDefault();

                    //Checking if there is enough money available for allocation (Troeslen & Japikse, 2021)
                    if (lastTotalReceived < money.Amount)
                    {
                        ViewBag.Error = "Not enough money available for allocation";
                        return View();
                    }

                    //Creating a new allocate money object and add it to the database (Troeslen & Japikse, 2021)
                    TblAllocateMoney m = new TblAllocateMoney()
                    {
                        Disasters = money.Disasters,
                        Amount = money.Amount,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblAllocateMoney.Add(m);
                    Poe.SaveChanges();

                    //Updating the AllocatedMoney field in the corresponding disaster (Troeslen & Japikse, 2021)
                    var disasterToUpdate = Poe.TblDisasters.FirstOrDefault(d => d.NameOfDisaster == money.Disasters && d.Status == "Active");
                    if (disasterToUpdate != null)
                    {
                        disasterToUpdate.AllocatedMoney += money.Amount;
                        Poe.SaveChanges();

                        //Decrease the TotalReceived field for relevant donations (Troeslen & Japikse, 2021)
                        var donationsToUpdate = Poe.TblMonetaryDonations.Where(d => d.Username == DisplayUsername.passUsername);
                        foreach (var donation in donationsToUpdate)
                        {
                            donation.TotalReceived -= money.Amount;
                        }
                        Poe.SaveChanges();
                    }

                    //Redirecting to the ViewMoney action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewMoney");
                }
            }
            catch (Exception ex) //Catching specific exception type (Troeslen & Japikse, 2021)
            {
                ViewBag.Error = ex.Message; //Displaying the exception message in ViewBag.Error (Troeslen & Japikse, 2021)
                return View();
            }
        }

        //Action method for displaying the list of money allocation (Troeslen & Japikse, 2021)
        public IActionResult ViewMoney()
        {
            List<TblAllocateMoney> temp = Poe.TblAllocateMoney.ToList();
            return View(temp);
        }
        //Action method for displaying details of a specific money allocation (Troeslen & Japikse, 2021)
        public IActionResult DetailsMoney(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the money allocation with the specified id (Troeslen & Japikse, 2021)
            TblAllocateMoney tblAllocate = Poe.TblAllocateMoney.Where(e => e.AllocateMoneyID == id).Single();
            return View(tblAllocate);
        }

        //POST action method for handling the details of a money allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsMoney(TblAllocateMoney tblAllocateMoney)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified money allocation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblAllocateMoney);
            Poe.SaveChanges();
            return RedirectToAction("ViewMoney", "AllocateMoney");
        }
    }
}
