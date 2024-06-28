using System.ComponentModel.DataAnnotations;


namespace Entidades
{
    public class Zapato
    {
        // ATRIBUTOS:

        [Key]
        public int Id_Zapato { get; set; }


        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }


        [Required(ErrorMessage = "El Precio es obligatorio.")]
        public double Precio { get; set; }


        // Tabla DetalleCompra Asociada A Esta:
        public virtual List<DetalleCompra> Lista_DetalleCompra { get; set; }

        

        // CONSTRUCTOR:
        public Zapato()
        {
            Lista_DetalleCompra = new List<DetalleCompra>();
        }
    }
}
