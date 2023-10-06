using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class CategoryController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to add a category (Troeslen & Japikse, 2021)
        public IActionResult AddCategory()
        {
            return View();
        }

        //POST action method for handling the submission of a new category (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddCategory(TblCategory category)
        {
            try
            {
                //Checking if the category name field is empty (Troeslen & Japikse, 2021)
                if (category.CategoryNames == null)
                {
                    ViewBag.Error = "Please enter all fields";
                    return View();
                }
                else
                {
                    //Creating a new category object and add it to the database (Troeslen & Japikse, 2021)
                    TblCategory m = new TblCategory()
                    {
                        CategoryNames = category.CategoryNames,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblCategories.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the ViewCategory action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewCategory");
                }
            }
            catch
            {
                ViewBag.Error = "Category Names already in the database";
                return View();
            }
        }

        //Action method for displaying the list of categories (Troeslen & Japikse, 2021)
        public IActionResult ViewCategory()
        {
            List<TblCategory> temp = Poe.TblCategories.ToList();
            return View(temp);
        }

        //Action method for displaying details of a specific category (Troeslen & Japikse, 2021)
        public IActionResult DetailsCategory(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the category with the specified id (Troeslen & Japikse, 2021)
            TblCategory tblCategory = Poe.TblCategories.Where(e => e.CategoriesId == id).Single();
            return View(tblCategory);
        }

        //POST action method for handling the deletion of a category (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsCategory(TblCategory tblCategory)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified category from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblCategory);
            Poe.SaveChanges();
            return RedirectToAction("ViewCategory", "Category");
        }

        //Action method for displaying the form to edit a category (Troeslen & Japikse, 2021)
        public IActionResult EditCategory(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the category with the specified id (Troeslen & Japikse, 2021)
            TblCategory tblCategory = Poe.TblCategories.Where(e => e.CategoriesId == id).Single();
            return View(tblCategory);
        }

        //POST action method for handling the submission of edited category information (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult EditCategory(TblCategory tblCategory)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Updating the category information in the database (Troeslen & Japikse, 2021)
            Poe.Update(tblCategory);
            Poe.SaveChanges();
            return RedirectToAction("ViewCategory", "Category");
        }

        //Action method for displaying the form to confirm deletion of a category (Troeslen & Japikse, 2021)
        public IActionResult DeleteCategory(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the category with the specified id (Troeslen & Japikse, 2021)
            TblCategory tblCategory = Poe.TblCategories.Where(e => e.CategoriesId == id).Single();
            return View(tblCategory);
        }

        //POST action method for handling the deletion of a category (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DeleteCategory(TblCategory tblCategory)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified category from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblCategory);
            Poe.SaveChanges();
            return RedirectToAction("ViewCategory", "Category");
        }
    }
}
