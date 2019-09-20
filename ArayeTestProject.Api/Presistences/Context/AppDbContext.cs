using ArayeTestProject.Api.Application.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ArayeTestProject.Api.Presistences.Context {
    public class AppDbContext : DbContext {

        public DbSet<City> Cities { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) {

        }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
        }
    }
}