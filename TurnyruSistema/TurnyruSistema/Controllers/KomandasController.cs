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
    public class KomandasController : Controller
    {
        private readonly TurnyruSistemaContext _context;

        public KomandasController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        // GET: Komandas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Komanda.ToListAsync());
        }

        // GET: Komandas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            

            var komanda = await _context.Komanda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komanda == null)
            {
                return NotFound();
            }
            var zaidejai = await _context.Zaidejas
                .Where<Zaidejas>(m => m.komandaId == komanda.Id).ToListAsync<Zaidejas>();
            Console.Write(zaidejai);
            komanda.zaidejai = zaidejai;
            return View(komanda);
        }

        // GET: Komandas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Komandas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Laimejimai,Pralaimejimai,Paveikslelis,pavadinimas,Prisijungimas,Slaptazodis,ElPastas,RegistracijosData,Id")] Komanda komanda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(komanda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(komanda);
        }

        public IActionResult AddPlayer(int id)
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlayer([Bind("vardas,slapyvardis,komandaId")] Zaidejas zaidejas, int id)
        {
            var komanda = await _context.Komanda
                .FirstOrDefaultAsync(m => m.Id == id);
            //zaidejas.komandaId = komanda.Id;
            
            if (ModelState.IsValid)
            {
                zaidejas.komandaId = id;
                _context.Add(zaidejas);
                //komanda.zaidejai.Add(zaidejas);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id });
        }

        // GET: Komandas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komanda = await _context.Komanda.FindAsync(id);
            if (komanda == null)
            {
                return NotFound();
            }
            return View(komanda);
        }

        // POST: Komandas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Laimejimai,Pralaimejimai,Paveikslelis,pavadinimas,Prisijungimas,Slaptazodis,ElPastas,RegistracijosData,Id")] Komanda komanda)
        {
            if (id != komanda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(komanda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KomandaExists(komanda.Id))
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
            return View(komanda);
        }

        public async Task<IActionResult> UpdatePlayerInfo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var zaidejas = await _context.Zaidejas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaidejas == null)
            {
                return NotFound();
            }
            
            //Console.Write(zaidejas);
            return View(zaidejas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePlayerinfo(int id, [Bind("Id,vardas,slapyvardis,komandaId")] Zaidejas zaidejas)
        {
            if (id != zaidejas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaidejas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaidejasExists(zaidejas.Id))
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
            return View(zaidejas);
        }


        public async Task<IActionResult> DeletePlayer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaidejas = await _context.Zaidejas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zaidejas == null)
            {
                return NotFound();
            }

            return View(zaidejas);
        }

        [HttpPost, ActionName("DeletePlayer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlayerConfirmed(int id)
        {
            var zaidejas = await _context.Zaidejas.FindAsync(id);
            _context.Zaidejas.Remove(zaidejas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Komandas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var komanda = await _context.Komanda
                .FirstOrDefaultAsync(m => m.Id == id);
            if (komanda == null)
            {
                return NotFound();
            }

            return View(komanda);
        }

        // POST: Komandas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var komanda = await _context.Komanda.FindAsync(id);
            _context.Komanda.Remove(komanda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KomandaExists(int id)
        {
            return _context.Komanda.Any(e => e.Id == id);
        }

        private bool ZaidejasExists(int id)
        {
            return _context.Zaidejas.Any(e => e.Id == id);
        }
    }
}
