using Acceso_Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica_Negocio
{
    public class CompraBL
    {
        // Objeto De La DB:
        private readonly CompraDAL _CompraDAL;


        // Constructor:
        public CompraBL(CompraDAL compraDAL)
        {
            _CompraDAL = compraDAL;
        }




        // ******* METODOS QUE MANDARAN OBJETOS A LAS VISTAS ********
        // **********************************************************

        // Manda Un Objeto Encontrado:
        public async Task<Compra> Obtener_PorId(Compra compra)
        {
            return await _CompraDAL.Obtener_PorId(compra);
        }


        // Manda Todos Los Objetos:
        public async Task<List<Compra>> Obtener_Todas()
        {
            return await _CompraDAL.Obtener_Todas();
        }


        // Lista De La Tabla Empleado Para ViewData:
        public async Task<List<Zapato>> Lista_Zapatos()
        {
            return await _CompraDAL.Lista_Zapatos();
        }



        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  ********
        // ********************************************************************

        // Recibe Un Objeto Lo Guarda En La DB:
        public async Task<int> Registrar_Compra(Compra compra)
        {
            return await _CompraDAL.Registrar_Compra(compra);
        }


        // Recibe Un Objeto Lo Busca Y Modifica El Encontrado Con El Nuevo:
        public async Task<int> Editar_Compra(Compra compra)
        {
            return await _CompraDAL.Editar_Compra(compra);
        }


        // Recibe Un Objeto Lo Busca Y Elimina El Encontrado:
        public async Task<int> Eliminar_Compra(Compra compra)
        {
            return await _CompraDAL.Eliminar_Compra(compra);
        }

    }
}
