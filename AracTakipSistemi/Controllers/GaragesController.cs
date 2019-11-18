using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AracTakipSistemi.Models;

namespace AracTakipSistemi.Controllers
{
    public class GaragesController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public GaragesController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Garages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Garages.ToListAsync());
        }

        // GET: Garages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garages = await _context.Garages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (garages == null)
            {
                return NotFound();
            }

            return View(garages);
        }

        // GET: Garages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Garages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CityID,Address,Coord_X,Coord_Y,Convenience")] Garages garages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(garages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(garages);
        }

        // GET: Garages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garages = await _context.Garages.SingleOrDefaultAsync(m => m.ID == id);
            if (garages == null)
            {
                return NotFound();
            }
            return View(garages);
        }

        // POST: Garages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,CityID,Address,Coord_X,Coord_Y,Convenience")] Garages garages)
        {
            if (id != garages.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(garages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GaragesExists(garages.ID))
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
            return View(garages);
        }

        // GET: Garages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var garages = await _context.Garages
                .SingleOrDefaultAsync(m => m.ID == id);
            if (garages == null)
            {
                return NotFound();
            }

            return View(garages);
        }

        // POST: Garages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var garages = await _context.Garages.SingleOrDefaultAsync(m => m.ID == id);
            _context.Garages.Remove(garages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GaragesExists(int id)
        {
            return _context.Garages.Any(e => e.ID == id);
        }
    }
}
