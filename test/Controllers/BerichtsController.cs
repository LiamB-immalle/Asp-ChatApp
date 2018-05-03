using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.Controllers
{
    public class BerichtsController : Controller
    {
        private readonly BerichtContext _context;

        public BerichtsController(BerichtContext context)
        {
            _context = context;
        }

        // GET: Berichts
        public async Task<IActionResult> Index(string contentSearchString)
        {
            //vul in
            var bericht = from m in _context.Bericht
                         select m;

            if (!String.IsNullOrEmpty(contentSearchString))
            {
                bericht = bericht.Where(s => s.Content.Contains(contentSearchString));
            }

            return View(await bericht.ToListAsync());
        }

        // GET: Berichts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Bericht
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bericht == null)
            {
                return NotFound();
            }

            return View(bericht);
        }

        // GET: Berichts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Berichts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] Bericht bericht)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bericht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bericht);
        }

        // GET: Berichts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Bericht.SingleOrDefaultAsync(m => m.ID == id);
            if (bericht == null)
            {
                return NotFound();
            }
            return View(bericht);
        }

        // POST: Berichts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Bericht bericht)
        {
            if (id != bericht.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bericht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BerichtExists(bericht.ID))
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
            return View(bericht);
        }

        // GET: Berichts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bericht = await _context.Bericht
                .SingleOrDefaultAsync(m => m.ID == id);
            if (bericht == null)
            {
                return NotFound();
            }

            return View(bericht);
        }

        // POST: Berichts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bericht = await _context.Bericht.SingleOrDefaultAsync(m => m.ID == id);
            _context.Bericht.Remove(bericht);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BerichtExists(int id)
        {
            return _context.Bericht.Any(e => e.ID == id);
        }
    }
}
