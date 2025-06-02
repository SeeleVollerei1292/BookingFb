using bookingfootball.Db_QL;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Data
{
    public class SbDbcontext : DbContext
    {

        public SbDbcontext(DbContextOptions<SbDbcontext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Sanbong> Sanbongs { get; set; }
    }
}