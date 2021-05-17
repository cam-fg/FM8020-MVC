using Microsoft.EntityFrameworkCore;
using FM8020_MVC.Models;

namespace FM8020_MVC.Data
{
    public class FMContext : DbContext
    {
        public FMContext(DbContextOptions<FMContext> options) : base(options) { }
        public DbSet<DefectModel> Defects { get; set; }
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<FacilityModel> Facilities { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }
    }
}