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

        [Route("Home/Dashboard/{timeView=week}/{weekStart?}/{filterString=Open}/{sortOrder=date_desc}/{componentFilter=all}")]
        public async Task<IActionResult> Dashboard(string timeView, string weekStart, string filterString, string sortOrder, string componentFilter)
        {
            ViewData["CurrentFilter"] = filterString;


            DateTime timeframeStart = DateTime.MinValue;
            DateTime timeframeEnd = DateTime.MaxValue;
            if (!String.IsNullOrEmpty(timeView))
            {
                if (timeView.Equals("alltime"))
                {
                    Debug.WriteLine("Alltime view");

                } else if (timeView.Equals("week"))
                {
                    Debug.WriteLine("Weekly view");
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
                    timeframeEnd = timeframeStart.AddDays(6);
                }
            }

            var defects = from d in _context.Defects.Where(d => (d.Timestamp >= timeframeStart) && (d.Timestamp < timeframeEnd))
                          select d;

            DefectViewModel defectVM = new DefectViewModel(
                defects.Count(),
                defects.Where(d => d.Done == false).Count(),
                defects.Where(d => d.Timestamp.Date == DateTime.Today).Count(),
                defects.Where(d => d.Done).Count(),
                timeframeStart,
                timeframeEnd
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

            if (!String.IsNullOrEmpty(componentFilter))  
                if (!componentFilter.Equals("all"))
                {
                    {
                        ComponentType defectType = (ComponentType)Enum.Parse(typeof(ComponentType), componentFilter);
                        defects = defects.Where(d => d.DefectType.Equals(defectType));
                    }
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
