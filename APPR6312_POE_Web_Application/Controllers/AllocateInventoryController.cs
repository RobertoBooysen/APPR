using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace APPR6312_POE_Web_Application.Controllers
{
    public class AllocateInventoryController : Controller
    {
        //Connection to database (The IIE, 2022)
        APPR6312_POEContext Poe = new APPR6312_POEContext();

        //Action method for displaying the form to allocate inventory (Troeslen & Japikse, 2021)
        public IActionResult AllocateInventory()
        {
            var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
            ViewBag.ActiveDisasters = activeDisasters;

            //Retrieving a list of goods in inventory table from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
            List<SelectListItem> goodsInventory = Poe.TblInventory
                .Select(c => new SelectListItem
                {
                    Value = c.GoodsInventory,
                    Text = c.GoodsInventory
                })
                .ToList();

            //Passing the list of inventory to the view using ViewBag (Troeslen & Japikse, 2021)
            ViewBag.GoodsInventory = goodsInventory;

            return View();
        }

        //POST action method for handling the submission of a new inventory allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult AllocateInventory(TblAllocateInventory allocateInventory)
        {
            try
            {
                //Checking if required fields are empty (Troeslen & Japikse, 2021)
                if (allocateInventory.Disasters == null || allocateInventory.GoodsInventory == null)
                {
                    ViewBag.Error = "Please enter all fields";

                    //Retrieving a list of goods in inventory table from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                    List<SelectListItem> goodsInventory = Poe.TblInventory
                        .Select(c => new SelectListItem
                        {
                            Value = c.GoodsInventory,
                            Text = c.GoodsInventory
                        })
                        .ToList();

                    //Passing the list of inventory to the view using ViewBag (Troeslen & Japikse, 2021)
                    ViewBag.GoodsInventory = goodsInventory;

                    return View();
                }
                else
                {
                    //Find the corresponding goods inventory (Troeslen & Japikse, 2021)
                    var inventoryDonation = Poe.TblInventory.FirstOrDefault(g => g.GoodsInventory == allocateInventory.GoodsInventory);
                    if (inventoryDonation != null)
                    {
                        //Check if there are enough items available (Troeslen & Japikse, 2021)
                        if (inventoryDonation.NumberOfInventoryGoods >= allocateInventory.NumberOfInventoryGoods)
                        {
                            //Create a new allocate inventory object (Troeslen & Japikse, 2021)
                            TblAllocateInventory m = new TblAllocateInventory()
                            {
                                Disasters = allocateInventory.Disasters,
                                GoodsInventory = allocateInventory.GoodsInventory,
                                NumberOfInventoryGoods = allocateInventory.NumberOfInventoryGoods,
                                Username = DisplayUsername.passUsername
                            };
                            //Add the inventory allocation to the database (Troeslen & Japikse, 2021)
                            Poe.TblAllocateInventory.Add(m);
                            //Decrement the NumberOfInventoryGoods in the goods donation (Troeslen & Japikse, 2021)
                            inventoryDonation.NumberOfInventoryGoods -= allocateInventory.NumberOfInventoryGoods;
                            Poe.SaveChanges();

                            //Update the AllocatedGoods field in the TblDisaster table (Troeslen & Japikse, 2021)
                            var disaster = Poe.TblDisasters.FirstOrDefault(d => d.NameOfDisaster == allocateInventory.Disasters);
                            if (disaster != null)
                            {
                                if (disaster.AllocatedGoods == "None")
                                    disaster.AllocatedGoods = allocateInventory.GoodsInventory + ": " + allocateInventory.NumberOfInventoryGoods;
                                else
                                    disaster.AllocatedGoods += ", " + allocateInventory.GoodsInventory + ": " + allocateInventory.NumberOfInventoryGoods;

                                Poe.SaveChanges();
                            }

                            //Redirecting to the ViewAllocatedInventory action (Troeslen & Japikse, 2021)
                            return RedirectToAction("ViewAllocatedInventory");
                        }
                        else
                        {
                            //Refresh the ViewBag.ActiveDisasters here (Troeslen & Japikse, 2021)
                            var activeDisasters = Poe.TblDisasters.Where(d => d.Status == "Active").Select(d => d.NameOfDisaster).ToList();
                            ViewBag.ActiveDisasters = activeDisasters;

                            ViewBag.Error = "Not enough items available in inventory";
                            //Retrieving a list of goods in inventory table from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                            List<SelectListItem> goodsInventory = Poe.TblInventory
                                .Select(c => new SelectListItem
                                {
                                    Value = c.GoodsInventory,
                                    Text = c.GoodsInventory
                                })
                                .ToList();

                            //Passing the list of inventory to the view using ViewBag (Troeslen & Japikse, 2021)
                            ViewBag.GoodsInventory = goodsInventory;
                            return View();
                        }

                    }
                    else
                    {
                        ViewBag.Error = "Inventory not found";
                        //Retrieving a list of goods in inventory table from the database and create SelectListItem objects (Troeslen & Japikse, 2021)
                        List<SelectListItem> goodsInventory = Poe.TblInventory
                            .Select(c => new SelectListItem
                            {
                                Value = c.GoodsInventory,
                                Text = c.GoodsInventory
                            })
                            .ToList();

                        //Passing the list of inventory to the view using ViewBag (Troeslen & Japikse, 2021)
                        ViewBag.GoodsInventory = goodsInventory;
                        return View();
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Inventory already in the database";
                return View();
            }
        }

        //Action method for displaying the list of goods allocation (Troeslen & Japikse, 2021)
        public IActionResult ViewAllocatedInventory()
        {
            List<TblAllocateInventory> temp = Poe.TblAllocateInventory.ToList();
            return View(temp);
        }

        //Action method for displaying details of a specific inventory allocation (Troeslen & Japikse, 2021)
        public IActionResult DetailsAllocateInventory(int id)
        {
            //Checking if the provided 'id' parameter is zero (0)
            //and return a 'NotFound' response if it evaluates to true (Troeslen & Japikse, 2021)
            if (id == 0)
            {
                return NotFound();
            }
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Retrieving the inventory allocation with the specified id (Troeslen & Japikse, 2021)
            TblAllocateInventory tblAllocate = Poe.TblAllocateInventory.Where(e => e.AllocateInventoryID == id).Single();
            return View(tblAllocate);
        }

        //POST action method for handling the details of a inventory allocation (Troeslen & Japikse, 2021)
        [HttpPost]
        public IActionResult DetailsAllocateInventory(TblAllocateInventory tblAllocateInventory)
        {
            //Connection to database (The IIE, 2022)
            APPR6312_POEContext Poe = new APPR6312_POEContext();
            //Removing the specified inventory allocation from the database (Troeslen & Japikse, 2021)
            Poe.Remove(tblAllocateInventory);
            Poe.SaveChanges();
            return RedirectToAction("ViewAllocatedInventory", "AllocateInventory");
        }
    }
}
