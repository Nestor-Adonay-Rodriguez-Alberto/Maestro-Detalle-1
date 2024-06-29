using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Compra
    {
        // ATRIBUTOS:

        [Key]
        public int Id_Compra { get; set; }

        [Required(ErrorMessage = "La Fecha es obligatoria.")]
        public DateTime FechaRealizada { get; set; }

        public int Correlativo { get; set; }

        public double Total { get; set; }

        // Tabla DetalleCompra Asociada A Esta:
        public virtual IList<DetalleCompra> Lista_DetalleCompra { get; set; }


        // CONSTRUCTOR:
        public Compra()
        {
            //Lista_DetalleCompra = new List<DetalleCompra>();
        }
    }
}
