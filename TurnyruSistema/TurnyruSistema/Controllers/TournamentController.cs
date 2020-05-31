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
            turnyras.Raundai = _context.Raundas
            .Include(r => r.Zaidimai)
            .Where(r => r.TurnyrasId == id).ToList();
            foreach (var raundas in turnyras.Raundai)
                foreach (var zaid in raundas.Zaidimai)
                {
                    zaid.Komanda1 = _context.Komanda.First(z => z.Id == zaid.Komanda1Id);
                    zaid.Komanda2 = _context.Komanda.First(z => z.Id == zaid.Komanda2Id);
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
            var organizer = new Organizatorius
            {
                ElPastas = "test",
                Prisijungimas = "test",
                Slaptazodis = "test",
                RegistracijosData = DateTime.Now,
                RodomasVardas = "Test"
            };
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
                CreateNewTournament(turnyras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizatoriusId"] = new SelectList(_context.Set<Organizatorius>(), "Id", "Id", turnyras.OrganizatoriusId);
            return View(turnyras);
        }
        public void CreateNewTournament(Turnyras temp)
        {
            _context.Add(temp);
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
                TempData["Message"] = "Turnyro duomenys sekmingai pakeisti";
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
            //_context.Turnyras.Remove(turnyras);
            
            var turnyrasKomanda = await _context.KomandaTurnyras
                .Where<KomandaTurnyras>(m => m.TurnyrasId == id).ToListAsync<KomandaTurnyras>();
            foreach(var item in turnyrasKomanda)
            _context.KomandaTurnyras.Remove(item);
            _context.Turnyras.Remove(turnyras);

            await _context.SaveChangesAsync();
            TempData["Message"] = "Turnyras pasalintas";
            return RedirectToAction(nameof(Index));
        }

        private bool TurnyrasExists(int id)
        {
            return _context.Turnyras.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GenerateRound(int id)
        {
            var turnyras = await GetTournament(id);
            turnyras.KompiuteriuZonos = _context.KompiuteriuZona.Where(kz => kz.TurnyrasId == id).ToList();
            var teamTournaments = await GetTeamTournament(id);
            var count = CountTeamsInTournament(teamTournaments);
            if (count >= MinimumTeams)
            {
                var Timetable = GenerateTimetable(turnyras, teamTournaments);
                TempData["Message"] = "Raundas sekmingai sukurtas";
                return RedirectToAction("Details", new { id });
            }
            else
            {
                throw new Exception("Not enough teams");
            }
        }

        private Raundas GenerateTimetable(Turnyras turnyras, List<KomandaTurnyras> teamTournaments)
        {
            var teams = _context.Komanda.Where(k => teamTournaments.Any(tt => tt.KomandaId == k.Id)).ToList();
            teams = teams.OrderBy(t => t.Laimejimai / (t.Pralaimejimai + 1)).ToList();
            var Reitingai = teams.Select(t => new { Reitingas = t.Laimejimai / (t.Pralaimejimai+1), id = t.Id });
            var rounds = _context.Raundas.Where(r => r.TurnyrasId == turnyras.Id);
            int CurrentRoundNr = rounds.Count() > 0 ? rounds.Max(r => r.Numeris) + 1 : 1;
            Raundas Raundas = CreateRound(turnyras.Id, CurrentRoundNr);
            Raundas.Zaidimai = new List<Zaidimas>();
            var TeamsSplit = new List<Tuple<int, int>>();
            if (rounds.Count() > 0)
            {
                var winTeam = new List<Tuple<int, int>>();
                CalculateWins(teams, rounds, winTeam);
                winTeam.OrderByDescending(wt => wt.Item1);
                PairTeams(TeamsSplit, winTeam);

            }
            else
            {
                while (teams.Count > 1)
                {
                    var two = teams.Take(2).ToArray();
                    TeamsSplit.Add(new Tuple<int, int>(two[0].Id, two[1].Id));
                    teams.Remove(two[0]);
                    teams.Remove(two[1]);
                }
            }
            if (TeamsSplit.Count*10 <= turnyras.KompiuteriuZonos.Sum(kz => kz.KompiuteriuSkaicius))
            {
                GenerateForNormal(turnyras, CurrentRoundNr, Raundas, TeamsSplit);
            }
            else
            {
                var compCount = turnyras.KompiuteriuZonos.Sum(kz => kz.KompiuteriuSkaicius);
                var TimeBlocks = Math.Ceiling(TeamsSplit.Count / (Math.Floor((double)compCount / 10)));
                var TimePerGame = Convert.ToInt32(Math.Floor(50 / (TimeBlocks)));
                CalculateWaitingTimes(TeamsSplit, TimePerGame);

                var RatedTeams = TeamsSplit.OrderBy(ts => Reitingai.First(r => ts.Item1 == r.id).Reitingas + Reitingai.First(r => ts.Item2 == r.id).Reitingas).ToList();
                //Generate games
                GenerateForAbnormal(turnyras, CurrentRoundNr, Raundas, RatedTeams, TimePerGame);
            }
            return Raundas;
        }

        private static void CalculateWaitingTimes(List<Tuple<int, int>> TeamsSplit, int TimePerGame)
        {
            var waitingTime = TeamsSplit.Select(ts => new Tuple<Tuple<int, int>, int>(ts, TimePerGame));

            var oWait = waitingTime.OrderBy(o => o.Item2);
        }

        private void GenerateForAbnormal(Turnyras turnyras, int CurrentRoundNr, Raundas Raundas, List<Tuple<int, int>> TeamsSplit, int TimePerGame)
        {
            var Zonos = new List<KompiuteriuZona>(turnyras.KompiuteriuZonos);
            var curZone = Zonos.First(z => z.KompiuteriuSkaicius >= 10);
            var remCount = curZone.KompiuteriuSkaicius;
            var incValue = 0;
            Zonos.Remove(curZone);
            foreach (var TeamPair in TeamsSplit)
            {
                if (remCount < 10)
                {
                    if (Zonos.Count > 0)
                    {
                        curZone = Zonos.First(z => z.KompiuteriuSkaicius >= 10);
                        Zonos.Remove(curZone);
                        remCount = curZone.KompiuteriuSkaicius;
                        
                    }
                    else
                    {
                        Zonos = new List<KompiuteriuZona>(turnyras.KompiuteriuZonos);
                        curZone = Zonos.First(z => z.KompiuteriuSkaicius >= 10);
                        remCount = curZone.KompiuteriuSkaicius;
                        incValue++;
                    }
                }
                DateTime gameStartTime = turnyras.PradziosData;
                gameStartTime = gameStartTime.AddMinutes(TimePerGame * (CurrentRoundNr - 1 + incValue));
                var game = CreateGame(TeamPair.Item1, TeamPair.Item2, curZone.Id, Raundas.Id, gameStartTime);
                Raundas.Zaidimai.Add(game);
                remCount -= 10;

            }
        }

        private void GenerateForNormal(Turnyras turnyras, int CurrentRoundNr, Raundas Raundas, List<Tuple<int, int>> TeamsSplit)
        {
            var Zonos = new List<KompiuteriuZona>(turnyras.KompiuteriuZonos);
            var curZone = Zonos.First(z => z.KompiuteriuSkaicius >= 10);
            var remCount = curZone.KompiuteriuSkaicius;
            Zonos.Remove(curZone);
            foreach (var TeamPair in TeamsSplit)
            {
                if (remCount < 10)
                {
                    curZone = Zonos.First(z => z.KompiuteriuSkaicius >= 10);
                    Zonos.Remove(curZone);
                    remCount = curZone.KompiuteriuSkaicius;
                }
                DateTime gameStartTime = turnyras.PradziosData;
                gameStartTime = gameStartTime.AddMinutes(50 * (CurrentRoundNr - 1));
                var game = CreateGame(TeamPair.Item1, TeamPair.Item2, curZone.Id, Raundas.Id, gameStartTime);
                Raundas.Zaidimai.Add(game);
                remCount -= 10;

            }
        }

        private static void PairTeams(List<Tuple<int, int>> TeamsSplit, List<Tuple<int, int>> winTeam)
        {
            while (winTeam.Count > 1)
            {
                var two = winTeam.Take(2).ToArray();
                TeamsSplit.Add(new Tuple<int, int>(two[0].Item2, two[1].Item2));
                winTeam.Remove(two[0]);
                winTeam.Remove(two[1]);
            }
        }

        private void CalculateWins(List<Komanda> teams, IQueryable<Raundas> rounds, List<Tuple<int, int>> winTeam)
        {
            foreach (var team in teams)
            {
                var wins = 0;
                foreach (var round in rounds)
                {
                    var Games = _context.Zaidimas.Where(z => z.RaundasId == round.Id && (z.Komanda1Id == team.Id || z.Komanda2Id == team.Id));
                    wins += Games.Where(g => g.LaimejusiKomanda == team.Id).Count();

                }
                winTeam.Add(new Tuple<int, int>(wins, team.Id));
            }
        }

        Zaidimas CreateGame(int Team1, int Team2, int Zone, int Round, DateTime time)
        {
            var game = new Zaidimas { Komanda1Id = Team1, Komanda2Id = Team2, KompiuteriuZonaId = Zone, RaundasId = Round, Laikas = time, Busena = Busena.Laukiama };
            _context.Zaidimas.Add(game);
            _context.SaveChanges();
            return game;
        }
        Raundas CreateRound(int Tournament, int Nr)
        {
            var Raundas = new Raundas { TurnyrasId = Tournament, Numeris = Nr };
            _context.Raundas.Add(Raundas);
            _context.SaveChanges();
            return Raundas;
        }
        private int CountTeamsInTournament(List<KomandaTurnyras> komandaTurnyras)
        {
            return komandaTurnyras.Where(kt => kt.Dalyvauja).Count();
        }

        public async Task<List<KomandaTurnyras>> GetTeamTournament(int id)
        {
            return await _context.KomandaTurnyras
                .Where(m => m.TurnyrasId == id).ToListAsync();
        }

        public async Task<IActionResult> TournamentTeams(int id)
        {
            return View(await _context.KomandaTurnyras
                .Where(m => m.TurnyrasId == id).ToListAsync());
        }

        public async Task<IActionResult> ConfirmTeam(int? id)
        {
            var patvirtinimas = await _context.KomandaTurnyras
                //.Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);

            patvirtinimas.Dalyvauja = true;

            if (ModelState.IsValid)
            {
                _context.Update(patvirtinimas);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Komandos registracija patvirtinta";
            }

            return RedirectToAction(nameof(Index)); ;

        }

        public async Task<IActionResult> WarnTeam(int id)
        {
            var ispejimas = await _context.KomandaTurnyras
                //.Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);

            var turnyras = new Turnyras { Id = id };

            if (ispejimas.Ispejimai >= 3)
            {
                RemoveTeamFromTournament(ispejimas);
                await _context.SaveChangesAsync();
                // ViewBag.Message = "Success";
                TempData["Message"] = "Komanda pasalinta";
                //return RedirectToAction(nameof(Index));
            }
            else
            {
                //ispejimas.Ispejimai++;
                IncreaseWarning(ispejimas);
                if (ModelState.IsValid)
                {
                    _context.Update(ispejimas);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Komandai duotas ispejimas";

                }
            }
   
            return RedirectToAction("SendMessageWarning", "Message");

        }
        private void RemoveTeamFromTournament(KomandaTurnyras temp)
        {
            _context.KomandaTurnyras.Remove(temp);
        }

        private void IncreaseWarning(KomandaTurnyras temp)
        {
            temp.Ispejimai++;
        }

        public async Task<IActionResult> Register(int? id, [Bind("Dalyvauja,Ispejimai,KomandaId,TurnyrasId")] KomandaTurnyras registracija)
        {
            var turnyras = await _context.Turnyras
                //.Include(t => t.Organizatorius)
                .FirstOrDefaultAsync(m => m.Id == id);

            registracija.Dalyvauja = false;
            registracija.Ispejimai = 0;
            registracija.KomandaId = (int)TempData["curUserId"];
            TempData.Keep();
            registracija.TurnyrasId = (int)id;

            if (ModelState.IsValid)
            {
                _context.Add(registracija);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Registracija sekminga";
            }


            return RedirectToAction("CreateMessage", "Message");
            //return RedirectToAction(nameof(Index)); 

        }

        public IActionResult EditTournament(int? id)
        {
            //computerZoneController = new ComputerZoneController(_context);
            return RedirectToAction("Details", "ComputerZone", new { id });
        }
    }
}
