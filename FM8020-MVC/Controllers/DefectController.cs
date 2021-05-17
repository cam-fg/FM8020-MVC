using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FM8020_MVC.Data;
using FM8020_MVC.Models;
using System.Diagnostics;

namespace FM8020_MVC.Controllers
{
    public class DefectController : Controller
    {
        private readonly FMContext _context;

        public DefectController(FMContext context)
        {
            _context = context;
        }

        // GET: Defect
        public async Task<IActionResult> Index()
        {
            return View(await _context.Defects.Include(m => m.Room).ThenInclude(m => m.Facility).ToListAsync());
        }

        // GET: Defect/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defectModel = await _context.Defects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defectModel == null)
            {
                return NotFound();
            }

            return View(defectModel);
        }

        // GET: Defect/Create
        [Route("Defect/Create/{roomId?}")]
        public IActionResult Create(int? roomId)
        {
            if (roomId.HasValue)
            {
                DefectModel defect = new DefectModel();
                RoomModel room = _context.Rooms.Find(roomId.Value);
                if (room != null)
                {
                    defect.Room = room;
                    return View(defect);
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Defect/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Defect/Create/{roomId}")]
        public async Task<IActionResult> Create(int roomId, [Bind("Id,Title,Description,Done")] DefectModel defectModel)
        {
            Debug.WriteLine("Room id: ", roomId);
            RoomModel room = _context.Rooms.Find(roomId);
            if (room != null)
            {
                defectModel.Room = room;
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("Valid ModelState");
                    _context.Add(defectModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            Debug.WriteLine("invalid ModelState");
            Debug.WriteLine(errors);
            return View(defectModel);
        }

        // GET: Defect/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defectModel = await _context.Defects.FindAsync(id);
            if (defectModel == null)
            {
                return NotFound();
            }
            return View(defectModel);
        }

        // POST: Defect/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Done")] DefectModel defectModel)
        {
            if (id != defectModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defectModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefectModelExists(defectModel.Id))
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
            return View(defectModel);
        }

        // GET: Defect/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defectModel = await _context.Defects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defectModel == null)
            {
                return NotFound();
            }

            return View(defectModel);
        }

        // POST: Defect/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defectModel = await _context.Defects.FindAsync(id);
            _context.Defects.Remove(defectModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefectModelExists(int id)
        {
            return _context.Defects.Any(e => e.Id == id);
        }
    }
}
