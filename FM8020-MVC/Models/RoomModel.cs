using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FM8020_MVC.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public FacilityModel Facility { get; set; }
    }
}
