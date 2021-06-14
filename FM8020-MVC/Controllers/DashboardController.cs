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
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly FMContext _context;

        public DashboardController(ILogger<DashboardController> logger, FMContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Alltime()
        {
            return View();
        }

        [Route("Dashboard/Weekly/{weekStart?}/{filterString=Open}/{sortOrder=date_desc}/{componentFilter=all}")]
        public async Task<IActionResult> Weekly(string weekStart, string filterString, string sortOrder, string componentFilter)
        {
            ViewData["CurrentFilter"] = filterString;

            DateTime timeframeStart;
            DateTime timeframeEnd;

            if (!String.IsNullOrEmpty(weekStart))
            {
                timeframeStart = DateTime.Parse(weekStart);
            }
            else
            {
                DayOfWeek weekday = DateTime.Today.DayOfWeek;
                int diff = (7 + (weekday - DayOfWeek.Monday)) % 7;
                timeframeStart = DateTime.Today.AddDays(-1 * diff).Date;
            }
            timeframeEnd = timeframeStart.AddDays(7).AddSeconds(-1);

            var defects = from d in _context.Defects.Where(d => (d.Timestamp >= timeframeStart) && (d.Timestamp <= timeframeEnd))
                          select d;

            DefectViewModel defectVM = new DefectViewModel(
                defects.Count(),
                defects.Where(d => d.Done == false).Count(),
                defects.Where(d => d.Timestamp.Date == DateTime.Today).Count(),
                defects.Where(d => d.Done).Count(),
                timeframeStart,
                timeframeEnd
                );

            defects = filterStatus(defects, filterString);
            defects = sortDefects(defects, sortOrder);
            defects = filterComponent(defects, componentFilter);

            defectVM.FilteredDefects = await defects.Include(m => m.Room).ThenInclude(m => m.Facility).ToListAsync();
            return View(defectVM);
        }

        [Route("Dashboard/Alltime/{filterString=Open}/{sortOrder=date_desc}/{componentFilter=all}")]
        public async Task<IActionResult> Alltime(string filterString, string sortOrder, string componentFilter)
        {
            ViewData["CurrentFilter"] = filterString;

            DateTime timeframeStart = DateTime.MinValue;
            DateTime timeframeEnd = DateTime.MaxValue;


            var defects = from d in _context.Defects
                          select d;

            DefectViewModel defectVM = new DefectViewModel(
                defects.Count(),
                defects.Where(d => d.Done == false).Count(),
                defects.Where(d => d.Timestamp.Date == DateTime.Today).Count(),
                defects.Where(d => d.Done).Count(),
                timeframeStart,
                timeframeEnd
                );

            defects = filterStatus(defects, filterString);
            defects = sortDefects(defects, sortOrder);
            defects = filterComponent(defects, componentFilter);

            defectVM.FilteredDefects = await defects.Include(m => m.Room).ThenInclude(m => m.Facility).ToListAsync();
            return View(defectVM);
        }

        private IQueryable<DefectModel> filterStatus(IQueryable<DefectModel> defects, string filterString)
        {
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
            return defects;
        }

        private IQueryable<DefectModel> sortDefects(IQueryable<DefectModel> defects, string sortOrder)
        {
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
            return (defects);
        }

        private IQueryable<DefectModel> filterComponent(IQueryable<DefectModel> defects, string componentFilter)
        {
            if (!String.IsNullOrEmpty(componentFilter))
            {
                if (!componentFilter.Equals("all"))
                {
                    {
                        ComponentType defectType = (ComponentType)Enum.Parse(typeof(ComponentType), componentFilter);
                        defects = defects.Where(d => d.DefectType.Equals(defectType));
                    }
                }
            }
            return defects;
        }
    }
}
