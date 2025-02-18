﻿using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class AllocateGoodsController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to allocate goods (Troeslen & Japikse, 2021)
        public IActionResult AllocatingGoods()
        {
            var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
            ViewBag.ActiveDisasters = activeDisasters;

            //Retrieving a list of goods from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
            List<SelectListItem> goods = Poe.TblGoodsDonations
                .Select(c => new SelectListItem
                {
                    Value = c.NameOfGood,
                    Text = c.NameOfGood
                })
                .ToList();

            //Passing the list of goods to the view using ViewBag (Troeslen & Japikse, 2021)
            ViewBag.Goods = goods;

            return View();
        }

        //POST action method for handling the submission of a new goods allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AllocatingGoods(TblAllocateGoods allocategoods)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (allocategoods.Disasters == null || allocategoods.Goods == null)
                {
                    ViewBag.Error = "Please enter all fields";

                    //Refreshing the ViewBag.ActiveDisasters (Troeslen & Japikse, 2021)
                    var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                    ViewBag.ActiveDisasters = activeDisasters;

                    //Retrieving a list of categories from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                    List<SelectListItem> goods = Poe.TblGoodsDonations
                        .Select(c => new SelectListItem
                        {
                            Value = c.NameOfGood,
                            Text = c.NameOfGood
                        })
                        .ToList();

                    //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
                    ViewBag.Goods = goods;

                    return View();
                }
                else
                {
                    //Find the corresponding goods donation (Troeslen & Japikse, 2021)
                    var goodsDonation = Poe.TblGoodsDonations.FirstOrDefault(g => g.NameOfGood == allocategoods.Goods);
                    if (goodsDonation != null)
                    {
                        //Check if there are enough items available (Troeslen & Japikse, 2021)
                        if (goodsDonation.NumberOfItems >= allocategoods.NumberOfGoods)
                        {
                            //Find existing allocation for the same disaster and goods (Troeslen & Japikse, 2021)
                            var existingAllocation = Poe.TblAllocateGoods.FirstOrDefault(a => a.Disasters == allocategoods.Disasters && a.Goods == allocategoods.Goods);

                            if (existingAllocation != null)
                            {
                                //Increment the existing allocation's quantity (Troeslen & Japikse, 2021)
                                existingAllocation.NumberOfGoods += allocategoods.NumberOfGoods;
                            }
                            else
                            {
                                //Create a new allocate goods object (Troeslen & Japikse, 2021)
                                TblAllocateGoods m = new TblAllocateGoods()
                                {
                                    Disasters = allocategoods.Disasters,
                                    Goods = allocategoods.Goods,
                                    NumberOfGoods = allocategoods.NumberOfGoods,
                                    Username = DisplayUsername.passUsername
                                };

                                // Add the goods allocation to the database (Troeslen & Japikse, 2021)
                                Poe.TblAllocateGoods.Add(m);
                            }

                            //Decrement the NumberOfItems in the goods donation (Troeslen & Japikse, 2021)
                            goodsDonation.NumberOfItems -= allocategoods.NumberOfGoods;
                            Poe.SaveChanges();

                            //Update the AllocatedGoods field in the TblDisaster table (Troeslen & Japikse, 2021)
                            var disaster = Poe.TblDisasters.FirstOrDefault(d => d.NameOfDisaster == allocategoods.Disasters);
                            if (disaster != null)
                            {
                                if (disaster.AllocatedGoods == "None")
                                {
                                    disaster.AllocatedGoods = allocategoods.Goods + ": " + allocategoods.NumberOfGoods;
                                }
                                else
                                {
                                    //Splitting the AllocatedGoods string to find existing allocations (Troeslen & Japikse, 2021)
                                    var allocatedGoodsList = disaster.AllocatedGoods.Split(',').Select(s => s.Trim()).ToList();
                                    bool updated = false;

                                    //Iterating through the existing allocations to find and update the matching one (Troeslen & Japikse, 2021)
                                    for (int i = 0; i < allocatedGoodsList.Count; i++)
                                    {
                                        var allocation = allocatedGoodsList[i].Split(':').Select(s => s.Trim()).ToList();
                                        if (allocation[0] == allocategoods.Goods)
                                        {
                                            //Incrementing the number of goods for the existing item (Troeslen & Japikse, 2021)
                                            int currentCount = int.Parse(allocation[1]);
                                            currentCount += allocategoods.NumberOfGoods;
                                            allocation[1] = currentCount.ToString();
                                            allocatedGoodsList[i] = string.Join(": ", allocation);
                                            updated = true;
                                            break;
                                        }
                                    }

                                    //If the allocation doesn't exist, add it to the list (Troeslen & Japikse, 2021)
                                    if (!updated)
                                    {
                                        allocatedGoodsList.Add(allocategoods.Goods + ": " + allocategoods.NumberOfGoods);
                                    }

                                    //Joining the updated allocation list and update the AllocatedGoods field (Troeslen & Japikse, 2021)
                                    disaster.AllocatedGoods = string.Join(", ", allocatedGoodsList);
                                }

                                Poe.SaveChanges();
                            }

                            //Redirecting to the ViewAllocatingGoods action (Troeslen & Japikse, 2021)
                            return RedirectToAction("ViewAllocatingGoods");
                        }
                        else
                        {
                            //Refreshing the ViewBag.ActiveDisasters (Troeslen & Japikse, 2021)
                            var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                            ViewBag.ActiveDisasters = activeDisasters;

                            ViewBag.Error = "Not enough items available in goods donations";
                            //Retrieve a list of categories from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                            List<SelectListItem> goods = Poe.TblGoodsDonations
                                .Select(c => new SelectListItem
                                {
                                    Value = c.NameOfGood,
                                    Text = c.NameOfGood
                                })
                                .ToList();
                            //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
                            ViewBag.Goods = goods;
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.Error = "Goods not found";

                        //Refreshing the ViewBag.ActiveDisasters (Troeslen & Japikse, 2021)
                        var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                        ViewBag.ActiveDisasters = activeDisasters;

                        //Retrieve a list of categories from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                        List<SelectListItem> goods = Poe.TblGoodsDonations
                            .Select(c => new SelectListItem
                            {
                                Value = c.NameOfGood,
                                Text = c.NameOfGood
                            })
                            .ToList();
                        //Passing the list of categories to the view using ViewBag (Troeslen & Japikse, 2021)
                        ViewBag.Goods = goods;
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Disaster already in the database";
                return View();
            }
        }

        //Action method for displaying the list of goods allocation (Troeslen & Japikse, 2021)
        public IActionResult ViewAllocatingGoods()
        {
            List<TblAllocateGoods> temp = Poe.TblAllocateGoods.ToList();
            return View(temp);
        }
        //Action method for displaying details of a specific goods allocation (Troeslen & Japikse, 2021)
        public IActionResult DetailsAllocatingGoods(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the goods allocation with the specified id (Troeslen & Japikse, 2021)
            TblAllocateGoods tblAllocateGoods = Poe.TblAllocateGoods.Where(e => e.AllocateGoodsID == id).Single();
            return View(tblAllocateGoods);
        }

        //POST action method for handling the details of a goods allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsAllocatingGoods(TblAllocateGoods tblAllocateGoods)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified goods allocation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblAllocateGoods);
            Poe.SaveChanges();
            return RedirectToAction("ViewAllocatingGoods", "AllocateGoods");
        }
    }
}
