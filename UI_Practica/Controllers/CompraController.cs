using Entidades;
using Logica_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI_Practica.Controllers
{
    public class CompraController : Controller
    {
        // Puente Para La DB:
        private readonly CompraBL _CompraBL;

        // Constructor:
        public CompraController(CompraBL compraBL)
        {
            _CompraBL = compraBL;
        } 
         


        // Manda Todos Los Registros De La Tabla:
        public async Task<IActionResult> Registros_Compras()
        {
            List<Compra> Objetos_Obtenidos = await _CompraBL.Obtener_Todas();

            return View(Objetos_Obtenidos);
        }

        // Manda Un Objeto Encontrado De La Tabla:
        public async Task<IActionResult> Detalle_Compra(int id)
        {
            Compra Objeto_Obtenido = await _CompraBL.Obtener_PorId(new Compra() { Id_Compra = id });

            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();

            ViewBag.Accion = "Detalle_Compra";
            return View(Objeto_Obtenido);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Registrar_Compra()
        {
            // Contamos Los Registros:
            List<Compra> Objetos_Obtenidos = await _CompraBL.Obtener_Todas();
            int Compras_Registradas = Objetos_Obtenidos.Count;


            // Objeto Con Informacion de Inicio:
            Compra Objeto_Inicio = new Compra();

            DateTime fechaHoraActual = DateTime.Now;
            DateTime fechaHoraActualizada = new DateTime(
                fechaHoraActual.Year,
                fechaHoraActual.Month,
                fechaHoraActual.Day,
                fechaHoraActual.Hour,
                fechaHoraActual.Minute,
                0
            );
            Objeto_Inicio.FechaRealizada = fechaHoraActualizada;

            Objeto_Inicio.Correlativo = Compras_Registradas + 1;

            Objeto_Inicio.Lista_DetalleCompra = new List<DetalleCompra>();
            Objeto_Inicio.Lista_DetalleCompra.Add(new DetalleCompra { Cantidad = 1, Precio_Producto = 0 });


            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();
            
            ViewBag.Accion = "Registrar_Compra";
            return View(Objeto_Inicio);
        }

        // Recibe Un Objeto Y Lo Guarda En La Tabla:
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar_Compra(Compra compra)
        {
            // Verificar Si Un Atributo Del Detalle Biene Vasio
            if (compra.Lista_DetalleCompra.Any(x => x.IdZapatoEnDetalle == 0 || x.Cantidad < 1 || x.Precio_Producto < 1))
            {

                if (compra.Lista_DetalleCompra.Any(x => x.IdZapatoEnDetalle == 0))
                {
                    TempData["ProductoRequerido"] = "Ingrese El Nombre Del Producto A LLevar.";
                }
                if (compra.Lista_DetalleCompra.Any(x => x.Cantidad < 1))
                {
                    TempData["CantidadRequerida"] = "Ingrese La Cantidad A LLevar.";
                }
                //if (compra.Lista_DetalleCompra.Any(x => x.Precio_Producto < 1))
                //{
                //    TempData["PrecioRequerido"] = "Ingrese El Precio Del Producto.";
                //}


                List<Zapato> Lista_Zapatoss = await _CompraBL.Lista_Zapatos();
                int n= Lista_Zapatoss.Count();
                ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();


                ViewBag.Accion = "Registrar_Compra";
                return View(compra);
            }
            else
            {
                compra.Total = compra.Lista_DetalleCompra.Sum(x => x.Cantidad * x.Precio_Producto);
                await _CompraBL.Registrar_Compra(compra);
            }

            return RedirectToAction("Registros_Compras", "Compra");
        }



        // Manda Un Objeto Encontrado De La Tabla
        public async Task<IActionResult> Editar_Compra(int id)
        {
            Compra Objeto_Obtenido = await _CompraBL.Obtener_PorId(new Compra() { Id_Compra = id });

            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();

            ViewBag.Accion = "Editar_Compra";
            return View(Objeto_Obtenido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Editar_Compra(Compra compra)
        {
            // Verificar Si Un Atributo Del Detalle Biene Vasio
            if (compra.Lista_DetalleCompra.Any(x => x.IdZapatoEnDetalle == 0 || x.Cantidad < 1 || x.Precio_Producto < 1))
            {

                if (compra.Lista_DetalleCompra.Any(x => x.IdZapatoEnDetalle == 0))
                {
                    TempData["ProductoRequerido"] = "Seleccione El Nombre Del Producto A LLevar.";
                }
                if (compra.Lista_DetalleCompra.Any(x => x.Cantidad < 1))
                {
                    TempData["CantidadRequerida"] = "Ingrese La Cantidad A LLevar.";
                }
                if (compra.Lista_DetalleCompra.Any(x => x.Precio_Producto < 1))
                {
                    TempData["PrecioRequerido"] = "Ingrese El Precio Del Producto.";
                }


                ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();


                ViewBag.Accion = "Editar_Compra";
                return View(compra);
            }
            else
            {
                await _CompraBL.Editar_Compra(compra);
            }

            return RedirectToAction("Registros_Compras", "Compra");
        }


        // Manda Un Objeto Encontrado De La Tabla:
        public async Task<IActionResult> Eliminar_Compra(int id)
        {
            Compra Objeto_Obtenido = await _CompraBL.Obtener_PorId(new Compra() { Id_Compra = id });

            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();

            ViewBag.Accion = "Eliminar_Compra";
            return View(Objeto_Obtenido);
        }


        // Recibe Un Objeto Y Lo Modifica En La Tabla:
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Eliminar_Compra(Compra compra)
        {
            await _CompraBL.Eliminar_Compra(compra);

            return RedirectToAction("Registros_Compras", "Compra");
        }




        //         METODOS PARA AGREGAR O ELIMINAR DETALLES A LA FACTURA
        // **********************************************************************

        [HttpPost]
        public async Task<IActionResult> Agregar_Detalle(Compra compra, string accion)
        {
            //Agregamos Detalle A La Lista:
            compra.Lista_DetalleCompra.Add(new DetalleCompra
            {
                Cantidad = 1,
                Precio_Producto = 0
            });

            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();

            ViewBag.Accion = accion;
            return PartialView("_Detalle_Compra", compra.Lista_DetalleCompra);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar_Detalle(Compra compra, int index, string accion)
        {
            DetalleCompra Objeto_Obtenido = compra.Lista_DetalleCompra[index];

            if (accion == "Editar_Compra" && Objeto_Obtenido.Id_DetalleCompra > 0)
            {
                Objeto_Obtenido.Id_DetalleCompra = Objeto_Obtenido.Id_DetalleCompra * -1;
            }
            else
            {
                compra.Lista_DetalleCompra.RemoveAt(index);
            }

            ViewData["Lista_Zapatos"] = await _CompraBL.Lista_Zapatos();

            ViewBag.Accion = accion;
            return PartialView("_Detalle_Compra", compra.Lista_DetalleCompra);
        }
    }
}
