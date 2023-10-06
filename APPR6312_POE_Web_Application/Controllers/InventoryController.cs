using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class InventoryController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to add inventory(Troeslen & Japikse, 2021)
        public IActionResult AddInventory()
        {
            return View();
        }

        //POST action method for handling the submission of inventory (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AddInventory(TblInventory inventory)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (inventory.GoodsInventory == null || inventory.PurchasedAmount == 0)
                {
                    ViewBag.Error = "Please enter all fields";

                    return View();
                }
                else
                {
                    //Creating a new inventory object and add it to the database (Troeslen & Japikse, 2021)
                    TblInventory m = new TblInventory()
                    {
                        GoodsInventory = inventory.GoodsInventory,
                        NumberOfInventoryGoods = inventory.NumberOfInventoryGoods,
                        PurchasedAmount = inventory.PurchasedAmount,
                        Username = DisplayUsername.passUsername
                    };
                    Poe.TblInventory.Add(m);
                    Poe.SaveChanges();
                    //Redirecting to the ViewInventory action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewInventory");
                }
            }
            catch
            {
                ViewBag.Error = "Inventory already in the database";
                return View();
            }
        }
        //Action method for displaying the list of inventory (Troeslen & Japikse, 2021)
        public IActionResult ViewInventory()
        {
            List<TblInventory> temp = Poe.TblInventory.ToList();
            return View(temp);
        }
        //Action method for displaying details of a specific inventory (Troeslen & Japikse, 2021)
        public IActionResult DetailsInventory(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the inventory with the specified id (Troeslen & Japikse, 2021)
            TblInventory tblInventory = Poe.TblInventory.Where(e => e.InventoryID == id).Single();
            return View(tblInventory);
        }

        //POST action method for handling the details of an inventory (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsInventory(TblInventory tblInventory)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified inventory from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblInventory);
            Poe.SaveChanges();
            return RedirectToAction("ViewInventory", "Inventory");
        }
    }
}
