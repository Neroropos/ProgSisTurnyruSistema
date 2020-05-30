using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TurnyruSistema.Models;

namespace TurnyruSistema.Controllers
{
    public class HomeController : Controller
    {
        private readonly TurnyruSistemaContext _context;
        public IActionResult Index()
        {
            if (TempData["curUserId"] == null)
            {
                TempData["curUserId"] = _context.Organizatorius.FirstOrDefault().Id;
                TempData["curUserType"] = "O";
            }
            TempData.Keep();
            return View();
        }
        public IActionResult SwapUser()
        {
            if ((string)TempData["curUserType"] == "O")
            {
                TempData["curUserId"] = _context.Komanda.FirstOrDefault().Id;
                TempData["curUserType"] = "K";
            }
            else
            {
                TempData["curUserId"] = _context.Organizatorius.FirstOrDefault().Id;
                TempData["curUserType"] = "O";
            }
            return RedirectToAction("Index");
        }
        public HomeController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
