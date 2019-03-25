using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPGSheet2.Data;
using RPGSheet2.Models;

namespace RPGSheet2.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IQueryable<IGrouping<string, TutorialPage>> tutquery;


        public TutorialsController(ApplicationDbContext context)
        {
            _context = context;
            tutquery = _context.TutorialPages.GroupBy((t) => t.Category);
        }

        // GET: Tutorials
        public async Task<IActionResult> Index(int? id)
        {
            ViewData["TutorialBar"] = true;
            ViewData["TutorialData"] = await tutquery.ToListAsync();

            var tutorialPage = await _context.TutorialPages
                .Include(t => t.Sections)
                .FirstOrDefaultAsync(m => m.ID == id);

            return View(tutorialPage);
        }

        // GET: Tutorials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Category")] TutorialPage tutorialPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tutorialPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tutorialPage);
        }
        // GET: Tutorials/Create
        public async Task<IActionResult> Add(int? ID)
        {
            if (ID.HasValue)
            {
                ViewData["PageID"] = ID;
                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Tutorials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Header,HTMLContent,PageID")] AddTutorialSection tutorialPage)
        {
            if (ModelState.IsValid)
            {
                if(tutorialPage.PageID <= 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                TutorialSection tut = tutorialPage;
                tut.Page = await _context.TutorialPages.FirstOrDefaultAsync(t => t.ID == tutorialPage.PageID);
                
                _context.Add(tut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index),new { id = tutorialPage.PageID });
            }
            return View(tutorialPage);
        }

        // GET: Tutorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorialSection = await _context.TutorialSections.Include(t => t.Page).FirstOrDefaultAsync(t => t.ID == id);
            if (tutorialSection == null)
            {
                return NotFound();
            }
            AddTutorialSection section = new AddTutorialSection(tutorialSection);
            return View(section);
        }

        // POST: Tutorials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Header,HTMLContent,PageID")] AddTutorialSection tutorialSection)
        {
            if (id != tutorialSection.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TutorialSection section = tutorialSection;
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index),new { id = tutorialSection.PageID });
            }
            return View(tutorialSection);
        }

        // GET: Tutorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tutorialPage = await _context.TutorialPages
                .FirstOrDefaultAsync(m => m.ID == id);
            if (tutorialPage == null)
            {
                return NotFound();
            }

            return View(tutorialPage);
        }

        // POST: Tutorials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tutorialPage = await _context.TutorialPages.FindAsync(id);
            _context.TutorialPages.Remove(tutorialPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialPageExists(int id)
        {
            return _context.TutorialPages.Any(e => e.ID == id);
        }
    }
}
