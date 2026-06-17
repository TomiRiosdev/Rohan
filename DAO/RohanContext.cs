using Models;
using Microsoft.EntityFrameworkCore;

namespace DAO;

public partial class RohanContext : DbContext
{
    public RohanContext()
    {
    }

    public RohanContext(DbContextOptions<RohanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bitacora> Bitacoras { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }

    public virtual DbSet<Lote> Lote { get; set; }

    public virtual DbSet<MovimientosStock> MovimientosStock { get; set; }
    public virtual DbSet<OrdenCompra> OrdenCompra { get; set; }

    public virtual DbSet<OrdenCompraDetalle> OrdenCompraDetalle { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ProductoProveedor> ProductoProveedor { get; set; }
    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<SolicitudPedido> SolicitudPedido { get; set; }

    public virtual DbSet<SolicitudPedidoDetalle> SolicitudPedidoDetalle { get; set; }
    public virtual DbSet<StockPorSucursal> StockPorSucursal { get; set; }

    public virtual DbSet<Sucursal> Sucursal { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimiento { get; set; }

    public virtual DbSet<TipoSucursal> TipoSucursal { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<VinculoSolicitudOc> VinculoSolicitudOc { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0PHAJEN\\MSSQLSERVER04;Database=RohanNegocio;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bitacora>(entity =>
        {
            entity.HasKey(e => e.IdBitacora);

            entity.ToTable("Bitacora");

            entity.Property(e => e.IdBitacora).ValueGeneratedNever();
            entity.Property(e => e.Detalle).IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Operacion).HasMaxLength(100);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria);

            entity.Property(e => e.IdCategoria).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EstadoSolicitud>(entity =>
        {
            entity.HasKey(e => e.IdEstadoSolicitud).HasName("PK_EstadoSolicitud_1");

            entity.ToTable("EstadoSolicitud");

            entity.Property(e => e.IdEstadoSolicitud).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.IdLote);

            entity.ToTable("Lote");

            entity.Property(e => e.IdLote).ValueGeneratedNever();
            entity.Property(e => e.CostoUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("datetime");
            entity.Property(e => e.NumeroLote)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdOrdenCompraDetalleNavigation).WithMany(p => p.Lote)
                .HasForeignKey(d => d.IdOrdenCompraDetalle)
                .HasConstraintName("FK_Lote_OrdenCompraDetalle");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Lote)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Lote_Producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Lote)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Lote_Proveedor");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.Lote)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_Lote_Sucursal");
        });

        modelBuilder.Entity<MovimientosStock>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento);

            entity.ToTable("MovimientosStock");

            entity.Property(e => e.IdMovimiento).ValueGeneratedNever();
            entity.Property(e => e.FechaMovimiento).HasColumnType("datetime");
            entity.Property(e => e.Observaciones)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.MovimientosStock)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK_MovimientosStock_Lote");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.MovimientosStockIdSucursalNavigations)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_MovimientosStock_Sucursal");

            entity.HasOne(d => d.IdSucursalDestinoNavigation).WithMany(p => p.MovimientosStockIdSucursalDestinoNavigations)
                .HasForeignKey(d => d.IdSucursalDestino)
                .HasConstraintName("FK_MovimientosStock_Sucursal1");

            entity.HasOne(d => d.IdSucursalOrigenNavigation).WithMany(p => p.MovimientosStockIdSucursalOrigenNavigations)
                .HasForeignKey(d => d.IdSucursalOrigen)
                .HasConstraintName("FK_MovimientosStock_Sucursal2");

            entity.HasOne(d => d.IdTipoMovimientoNavigation).WithMany(p => p.MovimientosStock)
                .HasForeignKey(d => d.IdTipoMovimiento)
                .HasConstraintName("FK_MovimientosStock_TipoMovimiento");
        });

        modelBuilder.Entity<OrdenCompra>(entity =>
        {
            entity.HasKey(e => e.IdOrdenCompra);

            entity.ToTable("OrdenCompra");

            entity.Property(e => e.IdOrdenCompra).ValueGeneratedNever();
            entity.Property(e => e.CostoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaOc)
                .HasColumnType("datetime")
                .HasColumnName("FechaOC");
            entity.Property(e => e.IdEstadoOc).HasColumnName("IdEstadoOC");

            entity.HasOne(d => d.IdEstadoSolicitudNavigation).WithMany(p => p.OrdenCompra)
                .HasForeignKey(d => d.IdEstadoOc)
                .HasConstraintName("FK_OrdenCompra_EstadoSolicitud");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.OrdenCompra)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_OrdenCompra_Proveedor");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.OrdenCompra)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_OrdenCompra_Sucursal");
        });

        modelBuilder.Entity<OrdenCompraDetalle>(entity =>
        {
            entity.HasKey(e => e.IdOrdenCompraDetalle);

            entity.ToTable("OrdenCompraDetalle");

            entity.Property(e => e.IdOrdenCompraDetalle).ValueGeneratedNever();
            entity.Property(e => e.PrecioPactado).HasColumnType("decimal(10, 2)");
    
            entity.HasOne(d => d.IdOrdenCompraNavigation)
                  .WithMany(p => p.OrdenCompraDetalle)
                  .HasForeignKey(d => d.IdOrdenCompra)
                  .HasConstraintName("FK_OrdenCompraDetalle_OrdenCompra");

            entity.HasOne(d => d.IdProductoNavigation)
                  .WithMany(p => p.OrdenCompraDetalle) 
                  .HasForeignKey(d => d.IdProducto)  
                  .HasConstraintName("FK_OrdenCompraDetalle_Producto"); 
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.CodigoSku).HasColumnName("CodigoSKU");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK_Producto_Categoria");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdUnidadMedida)
                .HasConstraintName("FK_Producto_UnidadMedida");
        });

        modelBuilder.Entity<ProductoProveedor>(entity =>
        {
            entity.HasKey(e => e.IdProductoProveedor);

            entity.ToTable("ProductoProveedor");

            entity.Property(e => e.IdProductoProveedor).ValueGeneratedNever();
            entity.Property(e => e.UltimoPrecioCompra).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoProveedor)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_ProductoProveedor_Producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ProductoProveedor)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_ProductoProveedor_Proveedor");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor);

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).ValueGeneratedNever();
            entity.Property(e => e.Cuit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CUIT");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SolicitudPedido>(entity =>
        {
            entity.HasKey(e => e.IdSolicitudPedido);

            entity.ToTable("SolicitudPedido");

            entity.Property(e => e.IdSolicitudPedido).ValueGeneratedNever();
            entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");

            entity.HasOne(d => d.IdSucursalNavigation)
                .WithMany(p => p.SolicitudPedido)
                .HasForeignKey(d => d.IdSucursal)
                .HasConstraintName("FK_SolicitudPedido_Sucursal");

         
            entity.HasOne(d => d.IdEstadoSolicitudNavigation)
                .WithMany(p => p.SolicitudPedido)
                .HasForeignKey(d => d.IdEstadoSolicitud)
                .HasConstraintName("FK_SolicitudPedido_EstadoSolicitud");

            entity.HasMany(d => d.SolicitudPedidoDetalle)
                .WithOne(p => p.IdSolicitudPedidoNavigation)
                .HasForeignKey(d => d.IdSolicitudPedido)
                .HasConstraintName("FK_SolicitudPedidoDetalle_SolicitudPedido");
        });

        modelBuilder.Entity<SolicitudPedidoDetalle>(entity =>
        {
       
            entity.HasKey(e => e.IdSolicitudPedidoDetalle);

            entity.ToTable("SolicitudPedidoDetalle");

            entity.Property(e => e.IdSolicitudPedidoDetalle).ValueGeneratedNever();

        
            entity.HasOne(d => d.IdProductoNavigation)
                .WithMany(p => p.SolicitudPedidoDetalle)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_SolicitudPedidoDetalle_Producto");

            entity.HasOne(d => d.IdSolicitudPedidoNavigation)
                .WithMany(p => p.SolicitudPedidoDetalle)
                .HasForeignKey(d => d.IdSolicitudPedido)
                .HasConstraintName("FK_SolicitudPedidoDetalle_SolicitudPedido");
        });

        modelBuilder.Entity<StockPorSucursal>(entity =>
        {
            entity.HasKey(e => e.IdStockPorSucursal);

            entity.ToTable("StockPorSucursal");

            entity.Property(e => e.IdStockPorSucursal).ValueGeneratedNever();

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.StockPorSucursal)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockPorSucursal_Producto");

            entity.HasOne(d => d.IdSucursalNavigation).WithMany(p => p.StockPorSucursal)
                .HasForeignKey(d => d.IdSucursal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockPorSucursal_Sucursal");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.IdSucursal);

            entity.ToTable("Sucursal");

            entity.Property(e => e.IdSucursal).ValueGeneratedNever();
            entity.Property(e => e.Direccion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Localidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoSucursalNavigation).WithMany(p => p.Sucursal)
                .HasForeignKey(d => d.IdTipoSucursal)
                .HasConstraintName("FK_Sucursal_TipoSucursal");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimiento);

            entity.ToTable("TipoMovimiento");

            entity.Property(e => e.IdTipoMovimiento).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoSucursal>(entity =>
        {
            entity.HasKey(e => e.IdTipoSucursal);

            entity.ToTable("TipoSucursal");

            entity.Property(e => e.IdTipoSucursal).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida);

            entity.Property(e => e.IdUnidadMedida).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VinculoSolicitudOc>(entity =>
        {
            entity.HasKey(e => e.IdVinculoSolicitudOc);

            entity.ToTable("VinculoSolicitudOC");

            entity.Property(e => e.IdVinculoSolicitudOc)
                .ValueGeneratedNever()
                .HasColumnName("IdVinculoSolicitudOC");

            entity.HasOne(d => d.IdOrdenCompraDetalleNavigation).WithMany(p => p.VinculoSolicitudOc)
                .HasForeignKey(d => d.IdOrdenCompraDetalle)
                .HasConstraintName("FK_VinculoSolicitudOC_OrdenCompraDetalle");

            entity.HasOne(d => d.IdSolicitudPedidoDetalleNavigation).WithMany(p => p.VinculoSolicitudOc)
                .HasForeignKey(d => d.IdSolicitudPedidoDetalle)
                .HasConstraintName("FK_VinculoSolicitudOC_SolicitudPedidoDetalle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
