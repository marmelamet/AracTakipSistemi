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
    public class ExpeditionsController : Controller
    {
        private readonly AppIdentityDbContext _context;

        public ExpeditionsController(AppIdentityDbContext context)
        {
            _context = context;
        }

        // GET: Expeditions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Expeditions.ToListAsync());
        }

        // GET: Expeditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expeditions = await _context.Expeditions
                .SingleOrDefaultAsync(m => m.ID == id);
            if (expeditions == null)
            {
                return NotFound();
            }

            return View(expeditions);
        }

        // GET: Expeditions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expeditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,UserID,DepartGarageID,ArrivalGarageID,DriverID,VehicleID,BeginTime,EndTime,IsActive")] Expeditions expeditions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expeditions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expeditions);
        }

        // GET: Expeditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expeditions = await _context.Expeditions.SingleOrDefaultAsync(m => m.ID == id);
            if (expeditions == null)
            {
                return NotFound();
            }
            return View(expeditions);
        }

        // POST: Expeditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,UserID,DepartGarageID,ArrivalGarageID,DriverID,VehicleID,BeginTime,EndTime,IsActive")] Expeditions expeditions)
        {
            if (id != expeditions.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expeditions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpeditionsExists(expeditions.ID))
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
            return View(expeditions);
        }

        // GET: Expeditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expeditions = await _context.Expeditions
                .SingleOrDefaultAsync(m => m.ID == id);
            if (expeditions == null)
            {
                return NotFound();
            }

            return View(expeditions);
        }

        // POST: Expeditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expeditions = await _context.Expeditions.SingleOrDefaultAsync(m => m.ID == id);
            _context.Expeditions.Remove(expeditions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpeditionsExists(int id)
        {
            return _context.Expeditions.Any(e => e.ID == id);
        }
    }
}
