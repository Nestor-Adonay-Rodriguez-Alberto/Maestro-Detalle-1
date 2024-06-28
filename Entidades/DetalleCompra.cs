using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DetalleCompra
    {
        // ATRIBUTOS:

        public int Id_DetalleCompra { get; set; }

        public int Cantidad { get; set; }

        public double Precio_Producto { get; set; }



        // Referencia Tabla Compra:
        [ForeignKey("Objeto_Compra")]
        public int IdCompraEnDetalle { get; set; }
        public virtual Compra Objeto_Compra { get; set; }


        // Referencia Tabla Zapato:
        [Required(ErrorMessage = "Seleccione Al Producto")]
        [ForeignKey("Objeto_Zapato")]
        public int IdZapatoEnDetalle { get; set; }
        public virtual Zapato Objeto_Zapato { get; set; }
    }
}
