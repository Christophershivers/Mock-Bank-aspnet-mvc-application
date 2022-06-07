using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Bank_of_Cardinal.Areas.Identity.Data;
using The_Bank_of_Cardinal.Models;

namespace The_Bank_of_Cardinal.Controllers
{



    [Authorize (Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CustomerInfoConnection _ci;
        private readonly DepositConnection _dc;
        private readonly TransactionConnection _tc;
        private readonly UserManager<CardinalUser> _userManager;
        private readonly SignInManager<CardinalUser> _signInManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager, CustomerInfoConnection ci, DepositConnection dc, TransactionConnection tc, UserManager<CardinalUser> userManager, SignInManager<CardinalUser> signInManager)
        {
            _roleManager = roleManager;
            _ci = ci;
            _dc = dc;
            _tc = tc;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        //This is the customer tab. it displays all the customers.
        public IActionResult Customers()
        {

            var listCusotmers = _ci.AspNetUsers.ToList();
            return View(listCusotmers);

        }

        [HttpGet]
        //You can edit user information.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var edit = _ci.AspNetUsers.FirstOrDefault(p => p.Id == id);
            if (edit == null)
            {
                return NotFound();
            }

            return View(edit);
        }
        [HttpPost]
        public IActionResult Edit(CustomerInfoModel ci)
        {


            _ci.AspNetUsers.Update(ci);
            _ci.SaveChanges();

            return RedirectToAction("Customers");

        }


        [HttpGet]
        //you can delete user information.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var delete = _ci.AspNetUsers.FirstOrDefault(p => p.Id == id);

            if (delete == null)
            {
                return NotFound();
            }


            return View(delete);

        }

        [HttpPost]
        public IActionResult Delete(CustomerInfoModel ci)
        {


            _ci.AspNetUsers.Remove(ci);
            _ci.SaveChanges();

            return RedirectToAction("Customers");

        }


        [HttpGet]
        //you can withdraw for the user and it'll show up as a administration withdrawl.
        public IActionResult Withdraw(string id)
        {
            var listBalance = _dc.AspNetUsers.Where(s => s.Id == id).FirstOrDefault();
            double Balance = _dc.AspNetUsers.Where(s => s.Id == id).Select(s => s.AccountBalance).FirstOrDefault();
            string regular = String.Format("{0:,0.00}", Balance);


            ViewBag.bal = regular;
            return View(listBalance);
        }

        [HttpPost]
        public IActionResult Withdraw(DepositModel model, TransactionModel tm)
        {
            var resultss = _dc.AspNetUsers.Where(s => s.Id == model.Id).FirstOrDefault();
            double Balance = _dc.AspNetUsers.Where(s => s.Id == model.Id).Select(s => s.AccountBalance).FirstOrDefault();
            double finalBalance = Balance - model.AccountBalance;




            tm.UserName = _dc.AspNetUsers.Where(s => s.Id == model.Id).Select(s => s.UserName).FirstOrDefault();
            string name = tm.UserName;
            tm.Description = "Administration withdrawl";
            tm.TransactionType = "Withdraw";
            tm.Amount = "$-" + model.AccountBalance.ToString();
            tm.Date = DateTime.Now;

            model.AccountBalance = finalBalance;
            model.Email = tm.UserName;
            model.UserName = tm.UserName;

            _tc.TransactionHistory.Add(tm);
            _tc.SaveChanges();


            _dc.AspNetUsers.Remove(resultss);
            _dc.AspNetUsers.Add(model);
            _dc.SaveChanges();

            return RedirectToAction("Customers");
        }

        [HttpGet]
        //You can deposit for the user and this will show up as administration deposit.
        public IActionResult Deposit(string id)
        {
            var listBalance = _dc.AspNetUsers.Where(s => s.Id == id).FirstOrDefault();
            double Balance = _dc.AspNetUsers.Where(s => s.Id == id).Select(s => s.AccountBalance).FirstOrDefault();
            string regular = String.Format("{0:,0.00}", Balance);


            ViewBag.bal = regular;
            return View(listBalance);
        }

        [HttpPost]
        public IActionResult Deposit(DepositModel model, TransactionModel tm)
        {
            var resultss = _dc.AspNetUsers.Where(s => s.Id == model.Id).FirstOrDefault();
            double Balance = _dc.AspNetUsers.Where(s => s.Id == model.Id).Select(s => s.AccountBalance).FirstOrDefault();
            double finalBalance = Balance + model.AccountBalance;




            tm.UserName = _dc.AspNetUsers.Where(s => s.Id == model.Id).Select(s => s.UserName).FirstOrDefault();
            string name = tm.UserName;
            tm.Description = "Administration deposit";
            tm.TransactionType = "deposit";
            tm.Amount = "$" + model.AccountBalance.ToString();
            tm.Date = DateTime.Now;

            model.AccountBalance = finalBalance;
            model.Email = tm.UserName;
            model.UserName = tm.UserName;

            _tc.TransactionHistory.Add(tm);
            _tc.SaveChanges();


            _dc.AspNetUsers.Remove(resultss);
            _dc.AspNetUsers.Add(model);
            _dc.SaveChanges();

            return RedirectToAction("Customers");
        }
    }
}
