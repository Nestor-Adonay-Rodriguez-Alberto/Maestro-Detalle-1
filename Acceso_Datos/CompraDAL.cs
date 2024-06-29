using Entidades;
using Microsoft.EntityFrameworkCore;


namespace Acceso_Datos
{
    public class CompraDAL
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;

        // Constructor:
        public CompraDAL(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }



        // ******* METODOS QUE MANDARAN OBJETOS A LAS VISTAS ********
        // **********************************************************

        // Manda Un Objeto Encontrado:
        public async Task<Compra> Obtener_PorId(Compra compra)
        {
            Compra? Objeto_Obtenido = await _MyDBcontext.Compras
                .Include(x => x.Lista_DetalleCompra)
                .ThenInclude(x => x.Objeto_Zapato)
                .FirstOrDefaultAsync(x => x.Id_Compra == compra.Id_Compra);

            if (Objeto_Obtenido != null)
            {
                return Objeto_Obtenido;
            }
            else
            {
                return new Compra();
            }
        }


        // Manda Todos Los Objetos:
        public async Task<List<Compra>> Obtener_Todas()
        {
            List<Compra> Objetos_Obtenidos = await _MyDBcontext.Compras.ToListAsync();

            return Objetos_Obtenidos;
        }

        // Lista De La Tabla Zapato Para ViewData:
        public async Task<List<Zapato>> Lista_Zapatos()
        {
            return await _MyDBcontext.Zapatos.ToListAsync();

        }


        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  ********
        // ********************************************************************

        // Recibe Un Objeto Lo Guarda En La DB:
        public async Task<int> Registrar_Compra(Compra compra)
        {
            _MyDBcontext.Add(compra);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // Recibe Un Objeto Lo Busca Y Modifica El Encontrado Con El Nuevo:
        public async Task<int> Editar_Compra(Compra compra)
        {
            Compra? Objeto_Obtenido = await _MyDBcontext.Compras
                .Include(x => x.Lista_DetalleCompra)
                .FirstOrDefaultAsync(x => x.Id_Compra == compra.Id_Compra);

            if (Objeto_Obtenido != null)
            {
                // NUEVOS DATOS DE FACTURA:
                Objeto_Obtenido.FechaRealizada = compra.FechaRealizada;
                Objeto_Obtenido.Correlativo = compra.Correlativo;
                Objeto_Obtenido.Total = compra.Total;

                // NUEVOS DETALLES AGREGARLOS A LAS LISTA:
                Agregar_Detalles(Objeto_Obtenido, compra);


                // DETALLES EXISTENTES DE LA LISTA "Podrian Traer Cambios":
                Editar_Detalles(Objeto_Obtenido, compra);


                // ELIMINAR LOS DETALLES DE LA LISTA:
                Eliminar_Detalles(Objeto_Obtenido, compra);


                _MyDBcontext.Update(Objeto_Obtenido);
            }

            //Recalcular 
            Objeto_Obtenido.Total = Objeto_Obtenido.Lista_DetalleCompra.Sum(s => s.Cantidad * s.Precio_Producto);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // METODOS PARA MODIFICAR EL DETALLE DE LA FACTURA
        // *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** ***

        // NUEVOS DETALLES AGREGARLOS A LAS LISTA:
        private void Agregar_Detalles(Compra Objeto_Obtenido, Compra compra)
        {
            IEnumerable<DetalleCompra> Detalles_Nuevos = compra.Lista_DetalleCompra.Where(s => s.Id_DetalleCompra == 0);
            foreach (DetalleCompra Detalle_Nuevo in Detalles_Nuevos)
            {
                Objeto_Obtenido.Lista_DetalleCompra.Add(Detalle_Nuevo);
            }
        }


        // DETALLES EXISTENTES DE LA LISTA "Podrian Traer Cambios":
        private void Editar_Detalles(Compra Objeto_Obtenido, Compra compra)
        {
            IEnumerable<DetalleCompra> Detalles_Lista = compra.Lista_DetalleCompra.Where(s => s.Id_DetalleCompra > 0);
            foreach (DetalleCompra Detalle in Detalles_Lista)
            {
                // Detalle En La Lista De La Factura Encontrada
                DetalleCompra? Detalle_EnLista = Objeto_Obtenido.Lista_DetalleCompra.FirstOrDefault(s => s.Id_DetalleCompra == Detalle.Id_DetalleCompra);

                Detalle_EnLista.IdZapatoEnDetalle = Detalle.IdZapatoEnDetalle;
                Detalle_EnLista.Cantidad = Detalle.Cantidad;
                Detalle_EnLista.Precio_Producto = Detalle.Precio_Producto;

            }
        }


        // ELIMINAR LOS DETALLES DE LA LISTA:
        private void Eliminar_Detalles(Compra Objeto_Obtenido, Compra compra)
        {
            IEnumerable<DetalleCompra> Detalles_Eliminar = compra.Lista_DetalleCompra.Where(s => s.Id_DetalleCompra < 0).ToList();

            if (Detalles_Eliminar != null)
            {
                foreach (DetalleCompra Detalle_Eliminar in Detalles_Eliminar)
                {
                    Detalle_Eliminar.Id_DetalleCompra = Detalle_Eliminar.Id_DetalleCompra * -1;

                    DetalleCompra? Detalle_ListaEliminar = Objeto_Obtenido.Lista_DetalleCompra.FirstOrDefault(s => s.Id_DetalleCompra == Detalle_Eliminar.Id_DetalleCompra);

                    _MyDBcontext.Remove(Detalle_ListaEliminar);
                    _MyDBcontext.SaveChanges();
                }
            }
        }

        // *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** *** ***

        // Recibe Un Objeto Lo Busca Y Elimina El Encontrado:
        public async Task<int> Eliminar_Compra(Compra compra)
        {
            Compra? Objeto_Obtenido = await _MyDBcontext.Compras.FirstOrDefaultAsync(m => m.Id_Compra == compra.Id_Compra);

            if (Objeto_Obtenido != null)
            {
                _MyDBcontext.Remove(Objeto_Obtenido);
            }

            return await _MyDBcontext.SaveChangesAsync();
        }
    }
}
