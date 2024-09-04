using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Repository
{
    public class WebApiInclusiveJourneyContext : DbContext
    {
        public WebApiInclusiveJourneyContext(DbContextOptions<WebApiInclusiveJourneyContext> options) : base(options) { }

        public DbSet<TabUsuario> tabUsuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TabUsuario>().ToTable("TabUsuario");
            modelBuilder.Entity<TabUsuario>();
        }
    }
}
