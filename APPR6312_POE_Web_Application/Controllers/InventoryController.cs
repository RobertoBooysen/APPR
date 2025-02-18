﻿using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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
            //Getting the last TotalReceived value from the last donation (Troeslen & Japikse, 2021)
            var lastTotalReceived = Poe.TblMonetaryDonations
                .OrderByDescending(d => d.DonationId)
                .Select(d => d.TotalReceived)
                .FirstOrDefault();

            //Add the lastTotalReceived value to a ViewBag (Troeslen & Japikse, 2021)
            ViewBag.LastTotalReceived = lastTotalReceived;

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

                    //Getting the last TotalReceived value from the last donation (Troeslen & Japikse, 2021)
                    var lastTotalReceived = Poe.TblMonetaryDonations
                        .OrderByDescending(d => d.DonationId)
                        .Select(d => d.TotalReceived)
                        .FirstOrDefault();

                    //Add the lastTotalReceived value to a ViewBag (Troeslen & Japikse, 2021)
                    ViewBag.LastTotalReceived = lastTotalReceived;

                    return View();
                }
                else
                {
                    //Check if the GoodsInventory already exists in the database (Troeslen & Japikse, 2021)
                    var existingInventory = Poe.TblInventory.FirstOrDefault(i => i.GoodsInventory == inventory.GoodsInventory);

                    if (existingInventory != null)
                    {
                        //Increment NumberOfInventoryGoods for the existing inventory (Troeslen & Japikse, 2021)
                        existingInventory.NumberOfInventoryGoods += inventory.NumberOfInventoryGoods;
                        existingInventory.PurchasedAmount += inventory.PurchasedAmount;
                    }
                    else
                    {
                        //Create a new inventory object (Troeslen & Japikse, 2021)
                        TblInventory m = new TblInventory()
                        {
                            GoodsInventory = inventory.GoodsInventory,
                            numberOfInventoryPurchased = inventory.NumberOfInventoryGoods,
                            NumberOfInventoryGoods = inventory.NumberOfInventoryGoods,
                            PurchasedAmount = inventory.PurchasedAmount,
                            Username = DisplayUsername.passUsername
                        };
                        Poe.TblInventory.Add(m);
                    }

                    //Calculate the total received for the user for inventory purchase (Troeslen & Japikse, 2021)
                    var totalReceivedForInventory = Poe.TblMonetaryDonations
                        .OrderByDescending(d => d.DonationId)
                        .Select(d => d.TotalReceived)
                        .FirstOrDefault();
                 

                    //Add the lastTotalReceived value to a ViewBag (Troeslen & Japikse, 2021)
                    ViewBag.LastTotalReceived = totalReceivedForInventory;

                    //Check if there is enough money available for the purchase (Troeslen & Japikse, 2021)
                    if (totalReceivedForInventory < inventory.PurchasedAmount)
                    {
                        ViewBag.Error = "Not enough money available for this purchase";
                        return View();
                    }

                    //Find the last donation for the current user (Troeslen & Japikse, 2021)
                    var lastDonationToUpdate = Poe.TblMonetaryDonations
                        .OrderByDescending(d => d.DonationId)
                        .FirstOrDefault();

                    if (lastDonationToUpdate != null)
                    {
                        var amountToDeduct = Math.Min(inventory.PurchasedAmount, lastDonationToUpdate.TotalReceived);
                        lastDonationToUpdate.TotalReceived -= amountToDeduct;
                        inventory.PurchasedAmount -= amountToDeduct;
                    }

                    Poe.SaveChanges();

                    //Redirecting to the ViewInventory action (Troeslen & Japikse, 2021)
                    return RedirectToAction("ViewInventory");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //Action method for displaying the list of inventory (Troeslen & Japikse, 2021)
        public IActionResult ViewInventory()
        {
            //Getting the last TotalReceived value from the last donation (Troeslen & Japikse, 2021)
            var lastTotalReceived = Poe.TblMonetaryDonations
                .OrderByDescending(d => d.DonationId)
                .Select(d => d.TotalReceived)
                .FirstOrDefault();

            //Add the lastTotalReceived value to a ViewBag (Troeslen & Japikse, 2021)
            ViewBag.LastTotalReceived = lastTotalReceived;

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
