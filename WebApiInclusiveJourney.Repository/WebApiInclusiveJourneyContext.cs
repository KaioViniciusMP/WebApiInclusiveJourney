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
        public DbSet<TabPessoa> tabPessoa { get; set; }
        public DbSet<TabGenero> tabGenero { get; set; }
        public DbSet<TabTipoDeficiencia> tabTipoDeficiencia { get; set; }
        public DbSet<TabPessoaTipo> tabPessoaTipo { get; set; }
        public DbSet<TabLugar> tabLugar { get; set; }
        public DbSet<TabPerson> tabPerson { get; set; }
        public DbSet<TabComments> tabComments { get; set; }
        public DbSet<TabZone> tabZone { get; set; }
        public DbSet<TabCategories> tabCategories { get; set; }
        public DbSet<TabPlaces> tabPlaces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<TabUsuario>().ToTable("TabUsuario");
            modelBuilder.Entity<TabPlaces>();
            modelBuilder.Entity<TabZone>();
            modelBuilder.Entity<TabCategories>();
            modelBuilder.Entity<TabPerson>();
            modelBuilder.Entity<TabComments>();
            modelBuilder.Entity<TabUsuario>();
            modelBuilder.Entity<TabPessoa>();
            modelBuilder.Entity<TabGenero>();
            modelBuilder.Entity<TabTipoDeficiencia>();
            modelBuilder.Entity<TabPessoaTipo>();
            modelBuilder.Entity<TabLugar>();
        }
    }
}
