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
    public class TournamentController : Controller
    {
        private readonly TurnyruSistemaContext _context;
        private static int MinimumTeams = 2;

        public TournamentController(TurnyruSistemaContext context)
        {
            _context = context;
        }

        // GET: Turnyras
        public async Task<IActionResult> Index()
        {
            var turnyruSistemaContext = _context.Turnyras;
            return View(await turnyruSistemaContext.ToListAsync());
        }

        // GET: Turnyras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnyras = await GetTournament(id);
            if (turnyras == null)
            {
                return NotFound();
            }

            return View(turnyras);
        }

        public async Task<Turnyras> GetTournament(int? id)
        {
            return await _context.Turnyras
                //.Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);
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

            var turnyras = await GetTournament(id);
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

            var turnyras = await GetTournament(id);
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
            var turnyras = await GetTournament(id);
            _context.Turnyras.Remove(turnyras);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnyrasExists(int id)
        {
            return _context.Turnyras.Any(e => e.Id == id);
        }

        public async Task <IActionResult> GenerateRound(int id)
        {
            var turnyras = await GetTournament(id);
            var teamTournaments = await GetTeamTournament(id);
            var count = CountTeamsInTournament(teamTournaments);
            if(count >= MinimumTeams)
            {
                var Timetable = GenerateTimetable(turnyras, teamTournaments);
                return View("Timetable", Timetable);
            }
            else
            {
                throw new Exception("Not enough teams");
            }
        }

        private TimetableVM GenerateTimetable(Turnyras turnyras, List<KomandaTurnyras> teamTournaments)
        {
            var teams = _context.Komanda.Where(k => teamTournaments.Any(tt => tt.KomandaId == k.Id)).ToList();
            teams = teams.OrderBy(t => t.Laimejimai / t.Pralaimejimai).ToList();
            var Reitingai = teams.Select(t => new { Reitingas = t.Laimejimai / t.Pralaimejimai, id = t.Id });
            var rounds = _context.Raundas.Where(r => r.TurnyrasId == turnyras.Id);
            if (rounds.Count() > 0)
            {
                var winTeam = new List<Tuple <int, int>>();
                foreach (var team in teams)
                {
                    foreach(var round in rounds)
                    {
                        var Games = _context.Zaidimas.Where(z => z.RaundasId == round.Id && (z.Komanda1Id == team.Id || z.Komanda2Id == team.Id));
                        var wins = Games.Where(g => g.LaimejusiKomanda == team.Id).Count();
                        winTeam.Add(new Tuple<int, int>(wins, team.Id));
                    }
                }
                winTeam.OrderByDescending(wt => wt.Item1);
                var TeamsSplit = new List<Tuple<int, int>>();
                while (winTeam.Count > 1)
                {
                    var two = winTeam.Take(2).ToArray();
                    TeamsSplit.Add(new Tuple<int, int>(two[0].Item2, two[1].Item2));
                    winTeam.Remove(two[0]);
                    winTeam.Remove(two[1]);
                }

            }
            throw new NotImplementedException();
        }

        private int CountTeamsInTournament(List<KomandaTurnyras> komandaTurnyras)
        {
            return komandaTurnyras.Count;
        }

        public async Task<List<KomandaTurnyras>> GetTeamTournament(int id)
        {
            return await _context.KomandaTurnyras
                .Where(m => m.TurnyrasId == id).ToListAsync();
        }
    }
}
