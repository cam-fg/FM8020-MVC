using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class DefectModel
    {
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public RoomModel Room { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime Timestamp{ get; set; }
        public ComponentType DefectType { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
    }
    public enum ComponentType
    {
        [Display(Name = "Sonstiges")]
        Other,
        [Display(Name = "Steckdose")]
        PlugSocket,
        [Display(Name = "Lampe")]
        Lamp,
        [Display(Name = "Fenster")]
        Window,
        Beamer,
        Computer,
        [Display(Name = "Kartenleser")]
        CardReader,
        [Display(Name = "Heizung")]
        Heater,
        [Display(Name = "Möbel")]
        Furniture
    }
}

