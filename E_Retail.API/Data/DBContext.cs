namespace E_Retail.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using E_Retail.API.Entities;

    namespace TuProyecto.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Persona> persona { get; set; }
            public DbSet<Usuario> usuario { get; set; }
            public DbSet<Ventas> ventas { get; set; }
            public DbSet<TipoUsuario> tipo_usuario { get; set; }
            public DbSet<Negocio> negocio { get; set; }
            public DbSet<Producto> producto { get; set; }
            public DbSet<Proveedor> proveedor { get; set; }
            public DbSet<Seguridad> seguridad { get; set; }
            public DbSet<TipoAccion> tipo_accion { get; set; }
            public DbSet<TipoProducto> tipo_producto { get; set; }
            public DbSet<DetalleVenta> detalle_venta { get; set; }
            // Agrega DbSet para otros modelos según sea necesario.

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Define las relaciones y restricciones según tus necesidades.
                modelBuilder.Entity<Usuario>()
                    .HasOne(u => u.Persona)
                    .WithMany()
                    .HasForeignKey(u => u.Cedula);

                modelBuilder.Entity<Usuario>()
                    .HasOne(u => u.TipoUsuario)
                    .WithMany()
                    .HasForeignKey(u => u.TipoUsuarioId);

                modelBuilder.Entity<Usuario>()
                    .HasOne(u => u.Negocio)
                    .WithMany()
                    .HasForeignKey(u => u.NegocioId);

                // Define otras relaciones según sea necesario.

                base.OnModelCreating(modelBuilder);
            }
        }
    }

}
