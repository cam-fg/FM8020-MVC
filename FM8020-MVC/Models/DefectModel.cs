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
        [Display(Name = "Raum")]
        public RoomModel Room { get; set; }
        [Display(Name = "Problem")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Uhrzeit")]
        public DateTime Timestamp{ get; set; }
        [Display(Name = "Kategorie")]
        public ComponentType DefectType { get; set; }
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        [Display(Name = "Zuständigkeit")]
        public String Responsibility { get; set; }
        [Display(Name = "Erledigt")]
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

