using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class FacilityModel
    {
        public int Id { get; set; }
        [Display(Name = "Adresse")]
        [BindProperty(SupportsGet = true)]
        public AddressModel Address { get; set; }
        public string Name { get; set; }
        [Display(Name = "Gebäudenummer")]
        public string FacilityCode { get; set; }
    }
}
