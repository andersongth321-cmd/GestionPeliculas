using Microsoft.EntityFrameworkCore;
using GestionPeliculas.Models;

namespace GestionPeliculas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<PeliculaActor> PeliculaActores => Set<PeliculaActor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculaActor>()
                .HasKey(pa => new { pa.PeliculaId, pa.ActorId });

            modelBuilder.Entity<PeliculaActor>()
                .HasOne(pa => pa.Pelicula)
                .WithMany(p => p.PeliculaActores)
                .HasForeignKey(pa => pa.PeliculaId);

            modelBuilder.Entity<PeliculaActor>()
                .HasOne(pa => pa.Actor)
                .WithMany(a => a.PeliculaActores)
                .HasForeignKey(pa => pa.ActorId);
        }
    }
}
