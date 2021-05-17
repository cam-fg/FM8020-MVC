using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class DefectModel
    {
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomModel Room { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
}
