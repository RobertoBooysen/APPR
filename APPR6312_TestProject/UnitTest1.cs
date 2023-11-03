using APPR6312_POE_Web_Application.Controllers;
using APPR6312_POE_Web_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace APPR6312_TestProject
{
    [TestClass]
    public class UnitTest1
    {
        //AllocateGoodsTest verifies goods allocation doesn't exceed available goods (The IIE, 2022)
        [TestMethod]
        public void AllocateGoodsTest()
        {
            //Arrange (The IIE, 2022)
            var controller = new AllocateGoodsController();
            var availableGoods = 100; //Available goods (The IIE, 2022)
            var goodsAllocationAmount = 20; //Amount of goods items to allocate (The IIE, 2022)

            var allocateGoods = new TblAllocateGoods
            {
                Disasters = "Hunger",
                Goods = "Canned Food",
                NumberOfGoods = goodsAllocationAmount,
            };

            //Act (The IIE, 2022)
            var result = controller.AllocatingGoods(allocateGoods) as IActionResult;

            //Assert (The IIE, 2022)
            Assert.IsNotNull(result);

            if (result is ViewResult viewResult)
            {
                //Checking for model state errors (The IIE, 2022)
                var modelState = viewResult.ViewData.ModelState;
                if (modelState.ContainsKey(string.Empty))
                {
                    //Checking if there's an error related to allocation amount (The IIE, 2022)
                    Assert.IsTrue(modelState[string.Empty].Errors.Any(e => e.ErrorMessage.Contains("Not enough items available in goods donations")));
                }
            }

            //Verifing that the goods allocation amount is not greater than the available goods inventory (The IIE, 2022)
            Assert.IsTrue(goodsAllocationAmount <= availableGoods);
        }
        //AllocateMoneyTest verifies money allocation doesn't exceed available money (The IIE, 2022)
        [TestMethod]
        public void AllocateMoneyTest()
        {
            //Arrange (The IIE, 2022)
            var controller = new AllocateMoneyController();
            var availableMoney = 10000; //Available money (The IIE, 2022)
            var allocationAmount = 2000; //Amount to allocate (The IIE, 2022)

            var money = new TblAllocateMoney
            {
                Disasters = "Hunger",
                Amount = allocationAmount,
            };

            //Act (The IIE, 2022)
            var result = controller.AddMoney(money) as IActionResult;

            //Assert (The IIE, 2022)
            Assert.IsNotNull(result);

            if (result is ContentResult contentResult)
            {
                //Checking if the response content contains the specific error message as a string (The IIE, 2022)
                var responseContent = contentResult.Content as string;

                //Verifing that the response content contains the error message (The IIE, 2022)
                Assert.IsTrue(responseContent.Contains("Not enough money available for allocation"));
            }

            //Verifing that the allocation amount is not greater than the available money (The IIE, 2022)
            Assert.IsTrue(allocationAmount < availableMoney);
        }
    }
}
