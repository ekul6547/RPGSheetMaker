using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPGSheet2.Data;
using RPGSheet2.Models;

namespace RPGSheet2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string q=null,byte? searchLimit=20,int? pageNumber=0)
        {
            bool loggedIn = User.IsLoggedIn();
            ViewData["LoggedIn"] = (bool?)loggedIn;
            if (loggedIn)
            {
                ViewData["MyGames"] = (await _context.GetSearchGamesForUserAsync(User.GetUserId()));
            }
            else
            {
                ViewData["MyGames"] = null;
            }

            ViewData["query"] = q;

            List<SearchGame> ret;

            if(!String.IsNullOrWhiteSpace(q))
            {
                List<int> results = (await _context.SearchGames(q));
                ret = (SearchGame.GenerateMany(_context,results.ToArray())).ToList();
            }
            else
            {
                Game[] results = (await _context.Games
                    .Include(g => g.Accesses)
                    .OrderBy(g => g.Accesses.Count)
                    .Skip(pageNumber.Value * searchLimit.Value)
                    .Take(searchLimit.Value)
                    .ToArrayAsync());

                ret = (SearchGame.GenerateMany(_context, results)).ToList();
            }

            return View(ret);
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
