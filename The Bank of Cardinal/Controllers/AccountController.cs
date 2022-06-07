
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using The_Bank_of_Cardinal.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using The_Bank_of_Cardinal.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace The_Bank_of_Cardinal.Controllers
{
   
       
   
    
    [Authorize]
    public class AccountController : Controller
    {
        private readonly TransactionConnection _tc;
        private readonly UserManager<CardinalUser> userManager;
        private readonly SignInManager<CardinalUser> signInManager;
        private readonly DepositConnection _dc;
        private readonly PayeeConnection _pc;
        private readonly UserAccountConnection _ua;
        private readonly CustomerInfoConnection _ci;
        public AccountController(TransactionConnection tc, UserManager<CardinalUser> userManager, SignInManager<CardinalUser> signInManager, DepositConnection dc, PayeeConnection pc, UserAccountConnection ua, CustomerInfoConnection ci)
        {
            _tc = tc;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _dc = dc;
            _pc = pc;
            _ua = ua;
            _ci = ci;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        //This displays the user transactions from the transaction history table
        public IActionResult Transactions()
        {
           //This retrieves the specific user from the database that is signed in. This is done by using the identity user manager and ef core query options.
            var results = _tc.TransactionHistory.Where(o => o.UserName == userManager.GetUserAsync(User).Result.UserName).ToList();
            return View(results);
        }

  

        
        [HttpGet]
        //This is the deposit tab where users can deposit money in their account
        public IActionResult Deposit(string Id)
        {
            //This helps the webpage pick out what user they are getting the money from in the AspNetUsers table.
            var resultss = _dc.AspNetUsers.Where(s => s.Id == Id).FirstOrDefault();
            
            return View(resultss);
        }
        [HttpPost]
        public IActionResult Deposit(DepositModel model, TransactionModel tm)
        {
            
            var resultss = _dc.AspNetUsers.Where(s => s.Id == model.Id).FirstOrDefault();
            //This take the number the user typed and add it to their account balance that's already set
            double balance = model.AccountBalance + userManager.GetUserAsync(User).Result.AccountBalance;

            
            tm.UserName = userManager.GetUserAsync(User).Result.UserName;
            string name = tm.UserName;
            tm.Description = "personal deposit";
            tm.TransactionType = "Deposit";
            //This stores the balance in the amount column in the transaction history table and converts it to a string
            tm.Amount = "$" + model.AccountBalance.ToString();
            //This sets the date and time this occured
            tm.Date = DateTime.Now;

           //these set the username email and accountbalance in the AspNetUsers table
            model.AccountBalance = balance;
            model.Email = tm.UserName;
            model.UserName = tm.UserName;

            //This adds the information that is hard typed and typed by the user into the transaction history table and then the next line saves it
            _tc.TransactionHistory.Add(tm);
            _tc.SaveChanges();

            //These do the same thing but for the AspNetUsers table
            _dc.AspNetUsers.Remove(resultss);
            _dc.AspNetUsers.Add(model);
            _dc.SaveChanges();

            return Content("This is your info \n" + 
                $"Name: {name} \n" + 
                $"Description: {tm.Description} \n" + 
                $"type: {tm.TransactionType} \n" + 
                $"Amount {tm.Amount} \n");
        }
        public IActionResult Transfers()
        {
            return View();
        }

        //This list the user's payyees that they entered
        public IActionResult UserPayee()
        {
           //This takes the user's payees who is signed in and then list them
            var payeeList = _pc.Payee.Where(o => o.UserName == userManager.GetUserAsync(User).Result.UserName).ToList();


            return View(payeeList);

        }
        
        [HttpGet]
        //this is where the user can edit, delete and add their payee information
        public IActionResult Edit(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var editPayee = _pc.Payee.FirstOrDefault(p => p.Id == id);
            if(editPayee == null)
            {
                return NotFound();
            }

            return View(editPayee);

        }
        
        [HttpPost]
        public IActionResult Edit(PayeeModel pm)
        {
           
            
            _pc.Payee.Update(pm);
            _pc.SaveChanges();

            return RedirectToAction("UserPayee");

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var deletePayee = _pc.Payee.FirstOrDefault(p => p.Id == id);

            if (deletePayee == null)
            {
                return NotFound();
            }


            return View(deletePayee);

        }
        [HttpPost]
        public IActionResult Delete(PayeeModel pm)
        {


            _pc.Payee.Remove(pm);
            _pc.SaveChanges();

            return RedirectToAction("UserPayee");

        }
        [HttpGet]
        public IActionResult AddPayee() {


            return View();
        
        }
        [HttpPost]
        public IActionResult AddPayee(PayeeModel pm)
        {
            if (ModelState.IsValid)
            {
                pm.UserName = userManager.GetUserAsync(User).Result.UserName;
                _pc.Payee.Add(pm);
                _pc.SaveChanges();

                return RedirectToAction("UserPayee");
            }
            
            return View();
        }
       
        
        
        
        [HttpGet]
        //This is where a user can send money to their payee
        public IActionResult Payee(string Id)
        {
            //This takes the user who is signed in payees and store them into a list and stores it into a ViewBag 
            List<PayeeModel> li = new List<PayeeModel>();
            li = _pc.Payee.Where(s => s.UserName == userManager.GetUserAsync(User).Result.UserName).ToList();
            ViewBag.payeeNames = li;
            return View();
        }
        [HttpPost]
        public IActionResult Payee(PayPayee pp, TransactionModel tm, DepositModel model)
        {

            var resultss = _dc.AspNetUsers.Where(s => s.Id == model.Id).FirstOrDefault();
            
                
                tm.Description = "You Transfered money to the user named " + pp.Name;
                string money = String.Format("{0:,0.00}", pp.Amount);
                tm.Amount = "$-" + money;
                tm.TransactionType = "User Pay";
                tm.Date = DateTime.Now;
                tm.UserName = userManager.GetUserAsync(User).Result.UserName;
                model.AccountBalance = userManager.GetUserAsync(User).Result.AccountBalance - pp.Amount;
                model.UserName = userManager.GetUserAsync(User).Result.UserName;
                model.Email = userManager.GetUserAsync(User).Result.UserName;


                //Adds to the transaction history table and saves it
                _tc.TransactionHistory.Add(tm);
                _tc.SaveChanges();
                //updates the user information in the AspNetUsers table. If this wasnt here then the user's information would be deleted.
                _dc.AspNetUsers.Remove(resultss);
                _dc.AspNetUsers.Add(model);
                _dc.SaveChanges();

            return View("Transfers");
        }

        [HttpGet]
        //This is where you can pay your bills
        public IActionResult BillPay(string Id)
        {
            var resultss = _dc.AspNetUsers.Where(s => s.Id == Id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public IActionResult BillPay(BillPayModel bp, TransactionModel tm, DepositModel model)
        {
           
                var resultss = _dc.AspNetUsers.Where(s => s.Id == model.Id).FirstOrDefault();
                tm.Amount = "$-" + bp.Amount.ToString();
                tm.UserName = userManager.GetUserAsync(User).Result.UserName;
                tm.Description = "you sent " + bp.Company + " a payment \n with the account number " + bp.AccountNumber;
                tm.TransactionType = "Bill pay";
                tm.Date = DateTime.Now;
                model.AccountBalance = userManager.GetUserAsync(User).Result.AccountBalance - bp.Amount;
                model.Email = tm.UserName;
                model.UserName = tm.UserName;

                _tc.TransactionHistory.Add(tm);
                _tc.SaveChanges();
                _dc.AspNetUsers.Remove(resultss);
                _dc.AspNetUsers.Add(model);
                _dc.SaveChanges();

                return RedirectToAction("Transfers");
            
        }

        public IActionResult AccountInfo(string id)
        {
            var list = _ua.AccountInfo.Where(a => a.UserName == id).ToList();
            
            return View(list);
        }

        [HttpGet]
        //You can edit user information.
        public IActionResult UpdateInfo(string id)
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
        public IActionResult UpdateInfo(CustomerInfoModel ci)
        {

            ci.AccountBalance = userManager.GetUserAsync(User).Result.AccountBalance;
            _ci.AspNetUsers.Update(ci);
            _ci.SaveChanges();

            return RedirectToAction("index");

        }
    }
}
