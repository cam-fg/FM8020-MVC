using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class FacilityModel
    {
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public AddressModel Address { get; set; }
        public string Name { get; set; }
        public string FacilityCode { get; set; }
    }
}
