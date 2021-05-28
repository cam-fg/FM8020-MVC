using FM8020_MVC.Data;
using FM8020_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FMContext _context;

        public HomeController(ILogger<HomeController> logger, FMContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Home/Dashboard/{filterString?}/{sortOrder?}")]
        public async Task<IActionResult> Dashboard(string filterString, string sortOrder)
        {
/*            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["RoomSortParm"] = sortOrder == "Room" ? "room_desc" : "Room";*/
            ViewData["CurrentFilter"] = filterString;
            var defects = from d in _context.Defects
                           select d;
            DefectViewModel defectVM = new DefectViewModel(
                defects.Count(),
                defects.Where(d => d.Done == false).Count(),
                defects.Where(d => d.Timestamp.Date == DateTime.Today).Count(),
                defects.Where(d => d.Done).Count()
                );
             

            if (!String.IsNullOrEmpty(filterString))
            {
                switch (filterString)
                {
                    case "Open":
                        defects = defects.Where(d => d.Done == false);
                        break;
                    case "New":
                        defects = defects.Where(d => d.Timestamp.Date == DateTime.Today);
                        break;
                    case "Done":
                        defects = defects.Where(d => d.Done);
                        break;
                    case "All":
                        break;
                    default:
                        defects = defects.Where(d => d.Done == false);
                        break;
                }
            }

            switch (sortOrder)
            {
                case "Date":
                    defects = defects.OrderBy(d => d.Timestamp);
                    break;
                case "date_desc":
                    defects = defects.OrderByDescending(d => d.Timestamp);
                    break;
                case "Room":
                    defects = defects.OrderBy(d => d.Room.RoomNumber);
                    break;
                default:
                    defects = defects.OrderBy(d => d.Timestamp);
                    break;
            }

            defectVM.FilteredDefects = await defects.Include(m => m.Room).ThenInclude(m => m.Facility).ToListAsync();
            return View(defectVM);           
            //return View(await defects.Include(m => m.Room).ThenInclude(m => m.Facility).ToListAsync());
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
