using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class DisasterController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to add a disaster (Troeslen & Japikse, 2021)
        public IActionResult AddDisaster()
        {
            return View();
        }

        //POST action method for handling the submission of a new disaster (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddDisaster(TblDisaster disaster)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (disaster.StartDate == null || disaster.EndDate == null || disaster.Location == null || disaster.Description == null)
                {
                    ViewBag.Error = "Please enter all fields";
                    return View();
                }
                else
                {
                    //Creating a new disaster object and add it to the database (Troeslen & Japikse, 2021)
                    TblDisaster m = new TblDisaster()
                    {
                        StartDate = disaster.StartDate,
                        EndDate = disaster.EndDate,
                        Location = disaster.Location,
                        Description = disaster.Description,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblDisasters.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the ViewDisaster action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewDisaster");
                }
            }
            catch
            {
                ViewBag.Error = "Disaster already in the database";
                return View();
            }
        }

        //Action method for displaying the list of disasters (Troeslen & Japikse, 2021)
        public IActionResult ViewDisaster()
        {
            List<TblDisaster> temp = Poe.TblDisasters.ToList();
            return View(temp);
        }

        //Action method for displaying details of a specific disaster (Troeslen & Japikse, 2021)
        public IActionResult DetailsDisaster(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the disaster with the specified id (Troeslen & Japikse, 2021)
            TblDisaster tblDisaster = Poe.TblDisasters.Where(e => e.DisasterId == id).Single();
            return View(tblDisaster);
        }

        //POST action method for handling the deletion of a disaster (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsDisaster(TblDisaster tblDisaster)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified disaster from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblDisaster);
            Poe.SaveChanges();
            return RedirectToAction("ViewDisaster", "Disaster");
        }

        //Action method for displaying the form to edit a disaster (Troeslen & Japikse, 2021)
        public IActionResult EditDisaster(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the disaster with the specified id (Troeslen & Japikse, 2021)
            TblDisaster tblDisaster = Poe.TblDisasters.Where(e => e.DisasterId == id).Single();
            return View(tblDisaster);
        }

        //POST action method for handling the submission of edited disaster information (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult EditDisaster(TblDisaster tblDisaster)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            // Update the disaster information in the database (Troeslen & Japikse, 2021)
            Poe.Update(tblDisaster);
            Poe.SaveChanges();
            return RedirectToAction("ViewDisaster", "Disaster");
        }

        //Action method for displaying the form to confirm deletion of a disaster (Troeslen & Japikse, 2021)
        public IActionResult DeleteDisaster(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the disaster with the specified id (Troeslen & Japikse, 2021)
            TblDisaster tblDisaster = Poe.TblDisasters.Where(e => e.DisasterId == id).Single();
            return View(tblDisaster);
        }

        //POST action method for handling the deletion of a disaster (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DeleteDisaster(TblDisaster tblDisaster)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified disaster from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblDisaster);
            Poe.SaveChanges();
            return RedirectToAction("ViewDisaster", "Disaster");
        }
    }
}
