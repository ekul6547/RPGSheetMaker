using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPGSheet2.Data;
using RPGSheet2.Models;

namespace RPGSheet2
{
    [Authorize]
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetSearchGamesForUserAsync(User.GetUserId()));
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .FirstOrDefaultAsync(m => m.SearchID == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // GET: Games/Join
        public async Task<IActionResult> Join(string id)
        {
            SearchGame sgame = await SearchGame.Generate(_context,id);
            if (sgame == null) return RedirectToAction(nameof(Index));
            return View(JoinGame.FromSearchGame(sgame, User.GetUserId()));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join([Bind("GameID,GameName,OwnerName,UserID,password")] JoinGame jgame)
        {
            if (ModelState.IsValid)
            {
                Game game = _context.Games.First((g) => g.ID == jgame.GameID);

                if (game.Password == jgame.password)
                {
                    await _context.GrantUserAccessToGame(game, jgame.UserID,true);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("password", "Password is incorrect.");
                    return View(jgame);
                }
            }
            ModelState.AddModelError("ModelOnly", "An unexpected error occured.");
            return View(jgame);
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Password")] CreateGame cgame)
        {
            if (ModelState.IsValid)
            {
                Game game = cgame.ToGame(User.GetUserId());
                _context.Add(game);
                await _context.SaveChangesAsync();
                game.AssignID();
                await _context.GrantUserAccessToGame(game, game.OwnerID);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(cgame);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = EditGame.GenerateAsync(_context, await _context.Games.FirstAsync((g) => g.SearchID == id));
            if (game == null)
            {
                return NotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GameID,SearchID,DisplayName,Description,Password")] EditGame egame)
        {
            if (id != egame.SearchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Game game = egame.ToGame(_context);
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(egame.SearchID))
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
            return View(egame);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = DeleteGame.Generate(_context, await _context.Games
                .FirstOrDefaultAsync(m => m.SearchID == id));
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var game = await _context.Games.FirstAsync((g) => g.SearchID == id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.ID == id);
        }
        private bool GameExists(string id)
        {
            return _context.Games.Any(e => e.SearchID == id);
        }
    }
}
