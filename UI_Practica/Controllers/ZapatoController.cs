using Entidades;
using Logica_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UI_Practica.Controllers
{
    public class ZapatoController : Controller
    {
        // Puente Para La DB:
        private readonly ZapatoBL _ZapatoBL;


        // Constructor:
        public ZapatoController(ZapatoBL zapatoBL)
        {
            _ZapatoBL = zapatoBL; 
        }




        // Manda Todos Los Registros De La Tabla:
        public async Task<IActionResult> Registros_Zapatos()
        {
            List<Zapato> Objetos_Obtenidos = await _ZapatoBL.Obtener_Todos();

            return View(Objetos_Obtenidos);
        }


        // Manda Un Objeto Encontrado De La Tabla:
        public async Task<IActionResult> Detalle_Zapato(int id)
        {
            Zapato Objeto_Obtenido = await _ZapatoBL.Obtener_PorId(new Zapato() { Id_Zapato = id });

            return View(Objeto_Obtenido);
        }


        [AllowAnonymous]
        public IActionResult Registrar_Zapato()
        {
            return View();
        }


        // Recibe Un Objeto Y Lo Guarda En La Tabla:
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Registrar_Zapato(Zapato zapato)
        {
            
            await _ZapatoBL.RegistrarZapato(zapato);

            return RedirectToAction("Registros_Zapatos", "Zapato");
        }

        // Manda Un Objeto Encontrado De La Tabla
        public async Task<IActionResult> Editar_Zapato(int id)
        {
            Zapato Objeto_Obtenido = await _ZapatoBL.Obtener_PorId(new Zapato() { Id_Zapato = id });

            return View(Objeto_Obtenido);
        }


        // Recibe El Objeto Que Fue Enviado Anteriormente:
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Editar_Zapato(Zapato zapato)
        {

            await _ZapatoBL.EditarZapato(zapato);


            return RedirectToAction("Registros_Zapatos", "Zapato");
        }


        // Manda Un Objeto Encontrado De La Tabla:
        public async Task<IActionResult> Eliminar_Zapato(int id)
        {
            Zapato Objeto_Obtenido = await _ZapatoBL.Obtener_PorId(new Zapato() { Id_Zapato = id });

            return View(Objeto_Obtenido);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Eliminar_Zapato(Zapato zapato)
        {
            await _ZapatoBL.EliminarZapato(zapato);

            return RedirectToAction("Registros_Zapatos", "Zapato");
        }
    }
}
