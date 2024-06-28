using Entidades;
using Microsoft.EntityFrameworkCore;


namespace Acceso_Datos
{
    public class MyDBcontext : DbContext
    {
        // Constructor:
        public MyDBcontext(DbContextOptions<MyDBcontext> options)
            : base(options)
        {

        }

        // Tablas En La DB:
        public DbSet<Zapato> Zapatos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }


    }
}
