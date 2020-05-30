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
            if (TempData["curUser"] == null)
                TempData["curUser"] = _context.Organizatorius.FirstOrDefault().Id;
            TempData.Keep();
            return View();
        }
        public IActionResult SwapUser()
        {
            var IsOrganizer = _context.Organizatorius.FirstOrDefault(o => o.Id == (int)TempData["curUser"]);
            if(IsOrganizer != null)
                TempData["curUser"] = _context.Komanda.FirstOrDefault().Id;
            else
                TempData["curUser"] = _context.Organizatorius.FirstOrDefault().Id;
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
