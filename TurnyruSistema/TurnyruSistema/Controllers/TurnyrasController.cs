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
    public class TurnyrasController : Controller
    {
        private readonly TurnyruSistemaContext _context;

        public TurnyrasController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        // GET: Turnyras
        public async Task<IActionResult> Index()
        {
            var turnyruSistemaContext = _context.Turnyras.Include(t => t.Organizatorius);
            return View(await turnyruSistemaContext.ToListAsync());
        }

        // GET: Turnyras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnyras = await _context.Turnyras
                .Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnyras == null)
            {
                return NotFound();
            }

            return View(turnyras);
        }
        public IActionResult FakeOrganizer()
        {
            var organizer = new Organizatorius { ElPastas = "test", Prisijungimas = "test", Slaptazodis = "test" ,
            RegistracijosData = DateTime.Now, RodomasVardas = "Test"};
            _context.Add(organizer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Turnyras/Create
        public IActionResult Create()
        {
            ViewData["OrganizatoriusId"] = new SelectList(_context.Set<Organizatorius>(), "Id", "Id");
            return View();
        }

        // POST: Turnyras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pavadinimas,Vieta,PradziosData,PabaigosData,OrganizatoriusId,Id")] Turnyras turnyras)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnyras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatoriusId"] = new SelectList(_context.Set<Organizatorius>(), "Id", "Id", turnyras.OrganizatoriusId);
            return View(turnyras);
        }

        // GET: Turnyras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnyras = await _context.Turnyras.FindAsync(id);
            if (turnyras == null)
            {
                return NotFound();
            }
            ViewData["OrganizatoriusId"] = new SelectList(_context.Set<Organizatorius>(), "Id", "Id", turnyras.OrganizatoriusId);
            return View(turnyras);
        }

        // POST: Turnyras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pavadinimas,Vieta,PradziosData,PabaigosData,OrganizatoriusId,Id")] Turnyras turnyras)
        {
            if (id != turnyras.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnyras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnyrasExists(turnyras.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatoriusId"] = new SelectList(_context.Set<Organizatorius>(), "Id", "Id", turnyras.OrganizatoriusId);
            return View(turnyras);
        }

        // GET: Turnyras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnyras = await _context.Turnyras
                .Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnyras == null)
            {
                return NotFound();
            }

            return View(turnyras);
        }

        // POST: Turnyras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnyras = await _context.Turnyras.FindAsync(id);
            _context.Turnyras.Remove(turnyras);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnyrasExists(int id)
        {
            return _context.Turnyras.Any(e => e.Id == id);
        }
    }
}
