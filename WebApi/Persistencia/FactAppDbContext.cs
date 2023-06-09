using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Dominio.Entidades;

namespace WebApi.Persistencia
{
    public partial class FactAppDbContext : DbContext
    {
        public FactAppDbContext()
        {
        }

        public FactAppDbContext(DbContextOptions<FactAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Inventario> Inventarios { get; set; } = null!;
        public virtual DbSet<Pago> Pagos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<RegistrosActividad> RegistrosActividads { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseNpgsql("host=pg-chmqlv64dad21k6fuv8g-a.oregon-postgres.render.com;port=5432;username=admin;password=NiokCt7llIFuQdiajhafTbUztOx6zFKC;database=dbstorage_g4mg");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("clientes", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.ToTable("detalle_facturas", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FacturaId).HasColumnName("factura_id");

                entity.Property(e => e.Impuestos)
                    .HasPrecision(10, 2)
                    .HasColumnName("impuestos");

                entity.Property(e => e.ProductoId).HasColumnName("producto_id");

                entity.Property(e => e.Subtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasPrecision(10, 2)
                    .HasColumnName("total");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.FacturaId)
                    .HasConstraintName("detalle_facturas_factura_id_fkey");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.DetalleFacturas)
                    .HasForeignKey(d => d.ProductoId)
                    .HasConstraintName("detalle_facturas_producto_id_fkey");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("facturas", "facturacion");

                entity.HasIndex(e => e.Numero, "facturas_numero_key")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClienteId).HasColumnName("cliente_id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.Impuestos)
                    .HasPrecision(10, 2)
                    .HasColumnName("impuestos");

                entity.Property(e => e.Numero)
                    .HasMaxLength(20)
                    .HasColumnName("numero");

                entity.Property(e => e.Subtotal)
                    .HasPrecision(10, 2)
                    .HasColumnName("subtotal");

                entity.Property(e => e.Total)
                    .HasPrecision(10, 2)
                    .HasColumnName("total");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("facturas_cliente_id_fkey");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.ProductoId)
                    .HasName("inventario_pkey");

                entity.ToTable("inventario", "facturacion");

                entity.Property(e => e.ProductoId)
                    .ValueGeneratedNever()
                    .HasColumnName("producto_id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.HasOne(d => d.Producto)
                    .WithOne(p => p.Inventario)
                    .HasForeignKey<Inventario>(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("inventario_producto_id_fkey");
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("pagos", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FacturaId).HasColumnName("factura_id");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasDefaultValueSql("CURRENT_DATE");

                entity.Property(e => e.MetodoPago)
                    .HasMaxLength(50)
                    .HasColumnName("metodo_pago");

                entity.Property(e => e.Monto)
                    .HasPrecision(10, 2)
                    .HasColumnName("monto");

                entity.HasOne(d => d.Factura)
                    .WithMany(p => p.Pagos)
                    .HasForeignKey(d => d.FacturaId)
                    .HasConstraintName("pagos_factura_id_fkey");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Impuesto)
                    .HasPrecision(5, 2)
                    .HasColumnName("impuesto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasPrecision(10, 2)
                    .HasColumnName("precio");
            });

            modelBuilder.Entity<RegistrosActividad>(entity =>
            {
                entity.ToTable("registros_actividad", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Accion)
                    .HasMaxLength(200)
                    .HasColumnName("accion");

                entity.Property(e => e.FechaHora)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("fecha_hora")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.RegistrosActividads)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("registros_actividad_usuario_id_fkey");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios", "facturacion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NivelAcceso).HasColumnName("nivel_acceso");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
