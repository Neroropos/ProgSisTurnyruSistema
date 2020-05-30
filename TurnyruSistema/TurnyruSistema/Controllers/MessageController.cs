using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TurnyruSistema.Models;

namespace TurnyruSistema.Controllers
{
    public class MessageController : Controller
    {
        private readonly TurnyruSistemaContext _context;

        public MessageController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        // GET: Messages
        public async Task<IActionResult> Messages()
        {
            return View(await _context.Zinute.ToListAsync());
        }

        public async Task<IActionResult> Message(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var message = await _context.Zinute
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tema,Turinys,IssiuntimoData,NaudotojasId")] Zinute message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Messages));
            }
            return View(message);
        }
    }
}