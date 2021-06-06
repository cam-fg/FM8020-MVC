using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        [Display(Name = "Stockwerk")]
        public int Floor { get; set; }
        [Display(Name = "Raumnummer")]
        public int RoomNumber { get; set; }
        [Display(Name = "Gebäude")]
        [BindProperty(SupportsGet = true)]
        public FacilityModel Facility { get; set; }
    }
}
