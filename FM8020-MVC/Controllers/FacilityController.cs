using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FM8020_MVC.Data;
using FM8020_MVC.Models;

namespace FM8020_MVC.Controllers
{
    public class FacilityController : Controller
    {
        private readonly FMContext _context;

        public FacilityController(FMContext context)
        {
            _context = context;
        }

        // GET: Facility
        public async Task<IActionResult> Index()
        {
            return View(await _context.Facilities.ToListAsync());
        }

        // GET: Facility/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // GET: Facility/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Facility/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FacilityCode")] FacilityModel facilityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilityModel);
        }

        // GET: Facility/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities.FindAsync(id);
            if (facilityModel == null)
            {
                return NotFound();
            }
            return View(facilityModel);
        }

        // POST: Facility/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FacilityCode")] FacilityModel facilityModel)
        {
            if (id != facilityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacilityModelExists(facilityModel.Id))
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
            return View(facilityModel);
        }

        // GET: Facility/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityModel = await _context.Facilities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facilityModel == null)
            {
                return NotFound();
            }

            return View(facilityModel);
        }

        // POST: Facility/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facilityModel = await _context.Facilities.FindAsync(id);
            _context.Facilities.Remove(facilityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacilityModelExists(int id)
        {
            return _context.Facilities.Any(e => e.Id == id);
        }
    }
}
