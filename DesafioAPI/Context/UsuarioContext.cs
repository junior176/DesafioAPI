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
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DbConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
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

                entity.Property(e => e.Ativo).HasColumnName("Ativo");

                entity.Property(e => e.CodigoLogin).HasColumnName("CodLogin");

                entity.Property(e => e.CodigoRecuperarSenha).HasColumnName("CodRecuperarSenha");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
