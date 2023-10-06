using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class MonetaryController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to add a monetary donation(Troeslen & Japikse, 2021)
        public IActionResult AddMonetary()
        {
            return View();
        }

        //POST action method for handling the submission of a new monetary donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddMonetary(TblMonetaryDonation donation)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (donation.FullName == null || donation.Date == null || donation.Amount == 0)
                {
                    ViewBag.Error = "Please enter all fields";
                    return View();
                }
                else
                {
                    //Check if FullName is "Anonymous" to determine anonymity (Troeslen & Japikse, 2021)
                    bool isAnonymous = donation.FullName == "Anonymous";

                    //Creating a new monetary donation object and add it to the database (Troeslen & Japikse, 2021)
                    TblMonetaryDonation m = new TblMonetaryDonation()
                    {
                        FullName = donation.FullName,
                        Date = donation.Date,
                        Amount = donation.Amount,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblMonetaryDonations.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the ViewMonetary action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewMonetary");
                }
            }
            catch
            {
                ViewBag.Error = "Monetary Donation already in the database";
                return View();
            }
        }

        //Action method for displaying the list of monetary donations (Troeslen & Japikse, 2021)
        public IActionResult ViewMonetary()
        {
            List<TblMonetaryDonation> temp = Poe.TblMonetaryDonations.ToList();
            return View(temp);
        }

        //Action method for displaying details of a specific monetary donation (Troeslen & Japikse, 2021)
        public IActionResult DetailsMonetary(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the monetary donation with the specified id (Troeslen & Japikse, 2021)
            TblMonetaryDonation tblMonetaryDonation = Poe.TblMonetaryDonations.Where(e => e.DonationId == id).Single();
            return View(tblMonetaryDonation);
        }

        //POST action method for handling the deletion of a monetary donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsMonetary(TblMonetaryDonation tblMonetary)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified monetary donation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblMonetary);
            Poe.SaveChanges();
            return RedirectToAction("ViewMonetary", "Monetary");
        }

        //Action method for displaying the form to edit a monetary donation (Troeslen & Japikse, 2021)
        public IActionResult EditMonetary(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the monetary donation with the specified id (Troeslen & Japikse, 2021)
            TblMonetaryDonation tblMonetaryDonation = Poe.TblMonetaryDonations.Where(e => e.DonationId == id).Single();
            return View(tblMonetaryDonation);
        }

        //POST action method for handling the submission of edited monetary donation information (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult EditMonetary(TblMonetaryDonation tblMonetaryDonation)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Updating the monetary donation information in the database (Troeslen & Japikse, 2021)
            Poe.Update(tblMonetaryDonation);
            Poe.SaveChanges();
            return RedirectToAction("ViewMonetary", "Monetary");
        }

        //Action method for displaying the form to confirm deletion of a monetary donation (Troeslen & Japikse, 2021)
        public IActionResult DeleteMonetary(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the monetary donation with the specified id (Troeslen & Japikse, 2021)
            TblMonetaryDonation tblMonetaryDonation = Poe.TblMonetaryDonations.Where(e => e.DonationId == id).Single();
            return View(tblMonetaryDonation);
        }

        //POST action method for handling the deletion of a monetary donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DeleteMonetary(TblMonetaryDonation tblMonetaryDonation)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified monetary donation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblMonetaryDonation);
            Poe.SaveChanges();
            return RedirectToAction("ViewMonetary", "Monetary");
        }
    }
}
