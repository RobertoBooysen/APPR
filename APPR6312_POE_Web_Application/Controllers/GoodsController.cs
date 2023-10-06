using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class GoodsController : Controller
    {
        //Connection to database (The IIE, 2022) 
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to add a goods donation (Troeslen & Japikse, 2021)
        public IActionResult AddGoods()
        {
            //Retrieving a list of categories from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
            List<SelectListItem> categories = Poe.TblCategories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryNames,
                    Text = c.CategoryNames
                })
                .ToList();

            //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
            ViewBag.Categories = categories;

            return View();
        }

        //POST action method for handling the submission of a new goods donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddGoods(TblGoodsDonation donation)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (donation.FullName == null || donation.Date == null || donation.NumberOfItems == 0 || donation.Category == null || donation.Description == null)
                {
                    ViewBag.Error = "Please enter all fields";

                    //Re-fetching and pass the list of categories to the view (Troeslen & Japikse, 2021)
                    List<SelectListItem> categories = Poe.TblCategories
                        .Select(c => new SelectListItem
                        {
                            Value = c.CategoryNames,
                            Text = c.CategoryNames
                        })
                        .ToList();

                    ViewBag.Categories = categories;

                    return View();
                }
                else
                {
                    //Check if FullName is "Anonymous" to determine anonymity (Troeslen & Japikse, 2021)
                    bool isAnonymous = donation.FullName == "Anonymous";

                    //Creating a new goods donation object and add it to the database (Troeslen & Japikse, 2021)
                    TblGoodsDonation m = new TblGoodsDonation()
                    {
                        FullName = donation.FullName,
                        Date = donation.Date,
                        NumberOfItems = donation.NumberOfItems,
                        Category = donation.Category,
                        Description = donation.Description,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblGoodsDonations.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the ViewGoods action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewGoods");
                }
            }
            catch
            {
                ViewBag.Error = "Goods Donation already in the database";
                return View();
            }
        }

        //Action method for displaying the list of goods donations (Troeslen & Japikse, 2021)
        public IActionResult ViewGoods()
        {
            List<TblGoodsDonation> temp = Poe.TblGoodsDonations.ToList();
            return View(temp);
        }

        //Action method for displaying details of a specific goods donation (Troeslen & Japikse, 2021)
        public IActionResult DetailsGoods(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the goods donation with the specified id (Troeslen & Japikse, 2021)
            TblGoodsDonation tblGoodsDonation = Poe.TblGoodsDonations.Where(e => e.GoodsId == id).Single();
            return View(tblGoodsDonation);
        }

        //POST action method for handling the deletion of a goods donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsGoods(TblGoodsDonation tblGoodsDonation)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified goods donation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblGoodsDonation);
            Poe.SaveChanges();
            return RedirectToAction("ViewGoods", "Goods");
        }

        //Action method for displaying the form to edit a goods donation (Troeslen & Japikse, 2021)
        public IActionResult EditGoods(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }

            TblGoodsDonation tblGoods = Poe.TblGoodsDonations.Where(e => e.GoodsId == id).SingleOrDefault();

            if (tblGoods == null)
            {
                return NotFound();
            }

            //Retrieving a list of categories from the database and create SelectListItem objects,
            //pre-selecting the current category of the goods donation (Troeslen & Japikse, 2021)
            List<SelectListItem> categories = Poe.TblCategories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryNames,
                    Text = c.CategoryNames,
                    Selected = (tblGoods.Category == c.CategoryNames)
                })
                .ToList();

            //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
            ViewBag.Categories = categories;

            return View(tblGoods);
        }

        //POST action method for handling the submission of edited goods donation information (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult EditGoods(TblGoodsDonation tblGoods)
        {
            if (ModelState.IsValid)
            {
                Poe.Update(tblGoods);
                Poe.SaveChanges();
                return RedirectToAction("ViewGoods", "Goods");
            }

            //Retrieving a list of categories from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
            List<SelectListItem> categories = Poe.TblCategories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryNames,
                    Text = c.CategoryNames
                })
                .ToList();

            //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
            ViewBag.Categories = categories;

            return View(tblGoods);
        }

        //Action method for displaying the form to confirm deletion of a goods donation (Troeslen & Japikse, 2021)
        public IActionResult DeleteGoods(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the goods donation with the specified id (Troeslen & Japikse, 2021)
            TblGoodsDonation tblGoodsDonation = Poe.TblGoodsDonations.Where(e => e.GoodsId == id).Single();
            return View(tblGoodsDonation);
        }

        //POST action method for handling the deletion of a goods donation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DeleteGoods(TblGoodsDonation tblGoodsDonation)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified goods donation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblGoodsDonation);
            Poe.SaveChanges();
            return RedirectToAction("ViewGoods", "Goods");
        }
    }
}
