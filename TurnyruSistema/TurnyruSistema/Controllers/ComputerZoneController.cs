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
    public class ComputerZoneController : Controller
    {
        private readonly TurnyruSistemaContext _context;

        public ComputerZoneController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        // GET: ComputerZone
        public async Task<IActionResult> Index()
        {
            var turnyruSistemaContext = _context.KompiuteriuZona.Include(k => k.Turnyras);
            return View(await turnyruSistemaContext.ToListAsync());
        }

        // GET: ComputerZone/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnyras = await _context.Turnyras.FirstOrDefaultAsync(m => m.Id == id);

            var kompiuteriuZonos = await _context.KompiuteriuZona
                .Where<KompiuteriuZona>(m => m.TurnyrasId == turnyras.Id).ToListAsync<KompiuteriuZona>();

            //var kompiuteriuZona = await _context.KompiuteriuZona
            //    .Include(k => k.Turnyras)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (kompiuteriuZonos == null)
            {
                return NotFound();
            }
            turnyras.KompiuteriuZonos = kompiuteriuZonos;
            return View(turnyras);
        }

        // GET: ComputerZone/Create
        public IActionResult Create2()
        {
            ViewData["TurnyrasId"] = new SelectList(_context.Turnyras, "Id", "Id");
            return View();
        }

        // POST: ComputerZone/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create2([Bind("Pavadinimas,KompiuteriuSkaicius,TurnyrasId,Id")] KompiuteriuZona kompiuteriuZona)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kompiuteriuZona);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TurnyrasId"] = new SelectList(_context.Turnyras, "Id", "Id", kompiuteriuZona.TurnyrasId);
            return View(kompiuteriuZona);
        }

        public IActionResult Create(int id)
        {

            var kompiuteriuZona = new KompiuteriuZona { TurnyrasId = id };
            return View(kompiuteriuZona);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pavadinimas,KompiuteriuSkaicius,TurnyrasId")] KompiuteriuZona kompiuteriuZona, int id)
        {
            var turnyras = await _context.Turnyras
                .FirstOrDefaultAsync(m => m.Id == id);
            //zaidejas.komandaId = komanda.Id;

            if (ModelState.IsValid)
            {
                kompiuteriuZona.TurnyrasId = id;
                CreateNewZone(kompiuteriuZona);
                //komanda.zaidejai.Add(zaidejas);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Kompiuteriu zona sekmingai sukurta";
                return RedirectToAction(nameof(Details),new { id});
            }
            return View(kompiuteriuZona);
        }

        public void CreateNewZone(KompiuteriuZona temp)
        {
            _context.Add(temp);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var kompiuteriuZona = await _context.KompiuteriuZona
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kompiuteriuZona == null)
            {
                return NotFound();
            }

            //Console.Write(zaidejas);
            return View(kompiuteriuZona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pavadinimas,KompiuteriuSkaicius,TurnyrasId")] KompiuteriuZona kompiuteriuZona)
        {
            if (id != kompiuteriuZona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kompiuteriuZona);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Kompiuteriu zona sekmingai pakeista";
                    //return RedirectToAction(nameof(Details), new { id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZonaExists(kompiuteriuZona.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = kompiuteriuZona.TurnyrasId });
                // return RedirectToAction(nameof(Details));
            }
            return View(kompiuteriuZona);
        }

        private bool ZonaExists(int id)
        {
            return _context.KompiuteriuZona.Any(e => e.Id == id);
        }


        // GET: ComputerZone/Edit/5
        public async Task<IActionResult> Edit2(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kompiuteriuZona = await _context.KompiuteriuZona.FindAsync(id);
            if (kompiuteriuZona == null)
            {
                return NotFound();
            }
            ViewData["TurnyrasId"] = new SelectList(_context.Turnyras, "Id", "Id", kompiuteriuZona.TurnyrasId);

            return View(kompiuteriuZona);
        }

        // POST: ComputerZone/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(int id, [Bind("Pavadinimas,KompiuteriuSkaicius,TurnyrasId,Id")] KompiuteriuZona kompiuteriuZona)
        {
            if (id != kompiuteriuZona.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kompiuteriuZona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KompiuteriuZonaExists(kompiuteriuZona.Id))
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
            ViewData["TurnyrasId"] = new SelectList(_context.Turnyras, "Id", "Id", kompiuteriuZona.TurnyrasId);
            return View(kompiuteriuZona);
        }

        // GET: ComputerZone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kompiuteriuZona = await _context.KompiuteriuZona
                .Include(k => k.Turnyras)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kompiuteriuZona == null)
            {
                return NotFound();
            }

            return View(kompiuteriuZona);
        }

        // POST: ComputerZone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kompiuteriuZona = await _context.KompiuteriuZona.FindAsync(id);
            _context.KompiuteriuZona.Remove(kompiuteriuZona);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Kompiuteriu zona sekmingai panaikinta";
            return RedirectToAction(nameof(Index));
        }

        private bool KompiuteriuZonaExists(int id)
        {
            return _context.KompiuteriuZona.Any(e => e.Id == id);
        }
    }
}
