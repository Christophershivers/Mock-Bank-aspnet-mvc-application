using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using The_Bank_of_Cardinal.Areas.Identity.Data;
using The_Bank_of_Cardinal.Models;

namespace The_Bank_of_Cardinal.Controllers
{
    public class HomeController : Controller
    {

        private readonly ConnectionStringClass _cc;
        private readonly ConnectionStringClass2 _bb;
        private readonly TransactionConnection _tcc;



        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ConnectionStringClass cc, ConnectionStringClass2 bb, TransactionConnection tcc)
        {
            _logger = logger;
            _cc = cc;
            _bb = bb;
            _tcc = tcc;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
