using Acceso_Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica_Negocio
{
    public class ZapatoBL
    {
        // Objeto De La DB:
        private readonly ZapatoDAL _ZapatoDAL;

        // Constructor:
        public ZapatoBL(ZapatoDAL zapatoDAL)
        {
            _ZapatoDAL = zapatoDAL;
        }




        // ******* METODOS QUE MANDARAN OBJETOS A LAS VISTAS ********
        // **********************************************************

        // Manda Un Objeto Encontrado:
        public async Task<Zapato> Obtener_PorId(Zapato zapato)
        {
            return await _ZapatoDAL.Obtener_PorId(zapato);
        }


        // Manda Todos Los Objetos:
        public async Task<List<Zapato>> Obtener_Todos()
        {
            return await _ZapatoDAL.Obtener_Todos();
        }




        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  ********
        // ********************************************************************

        // Recibe Un Objeto Lo Guarda En La DB:
        public async Task<int> RegistrarZapato(Zapato zapato)
        {
            return await _ZapatoDAL.RegistrarZapato(zapato);
        }


        // Recibe Un Objeto Lo Busca Y Modifica El Encontrado Con El Nuevo:
        public async Task<int> EditarZapato(Zapato zapato)
        {
            return await _ZapatoDAL.EditarZapato(zapato);
        }


        // Recibe Un Objeto Lo Busca Y Elimina El Encontrado:
        public async Task<int> EliminarZapato(Zapato zapato)
        {
            return await _ZapatoDAL.EliminarZapato(zapato);
        }

    }
}
