using DesafioAPI.Models;
using DesafioAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace DesafioAPI.Context
{
    public partial class UsuarioContext : DbContext
    {
        public UsuarioContext()
        {
        }

        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbDesafio.getConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.Id).IsClustered(false);

                entity.Property(u => u.Id).HasColumnName("Id");

                entity.Property(u => u.Nome).HasColumnName("Nome");

                entity.Property(u => u.Email).HasColumnName("Email").HasMaxLength(150);

                entity.Property(e => e.Senha).HasColumnName("Senha");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
