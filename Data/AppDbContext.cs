using Microsoft.EntityFrameworkCore;
using PlotsAndPlates.Backend.Models;

namespace PlotsAndPlates.Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //declaração de quais tabelas vamos acessar
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Prato> Pratos { get; set; }
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ID será gerado automaticamente
            modelBuilder.Entity<Prato>()
                .Property(p => p.Id)
                .UseIdentityColumn();


            modelBuilder.Entity<Avaliacao>()
                .Property(a => a.Id)
                .UseIdentityColumn();
        }
    }
}