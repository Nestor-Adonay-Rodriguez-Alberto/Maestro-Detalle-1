using Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acceso_Datos
{
    public class ZapatoDAL
    {
        // Representa La DB:
        private readonly MyDBcontext _MyDBcontext;

        // Constructor:
        public ZapatoDAL(MyDBcontext myDBcontext)
        {
            _MyDBcontext = myDBcontext;
        }



        // ******* METODOS QUE MANDARAN OBJETOS A LAS VISTAS ********
        // **********************************************************

        // Manda Un Objeto Encontrado:
        public async Task<Zapato> Obtener_PorId(Zapato zapato)
        {
            Zapato? Objeto_Obtenido = await _MyDBcontext.Zapatos.FirstOrDefaultAsync(x => x.Id_Zapato == zapato.Id_Zapato);

            if (Objeto_Obtenido != null)
            {
                return Objeto_Obtenido;
            }
            else
            {
                return new Zapato();
            }
        }


        // Manda Todos Los Objetos:
        public async Task<List<Zapato>> Obtener_Todos()
        {
            List<Zapato> Objetos_Obtenidos = await _MyDBcontext.Zapatos.ToListAsync();

            return Objetos_Obtenidos;
        }



        // *******  METODOS QUE RECIBIRAN OBJETOS Y MODIFICARAN LA DB  ********
        // ********************************************************************

        // Recibe Un Objeto Lo Guarda En La DB:
        public async Task<int> RegistrarZapato(Zapato zapato)
        {
            _MyDBcontext.Add(zapato);

            return await _MyDBcontext.SaveChangesAsync();
        }


        // Recibe Un Objeto Lo Busca Y Modifica El Encontrado Con El Nuevo:
        public async Task<int> EditarZapato(Zapato zapato)
        {
            Zapato? Objeto_Obtenido = await _MyDBcontext.Zapatos.FirstOrDefaultAsync(x => x.Id_Zapato == zapato.Id_Zapato);

            if (Objeto_Obtenido != null)
            {
                Objeto_Obtenido.Nombre = zapato.Nombre;
                Objeto_Obtenido.Precio = zapato.Precio;

                _MyDBcontext.Update(Objeto_Obtenido);
            }

            return await _MyDBcontext.SaveChangesAsync();
        }

        // Recibe Un Objeto Lo Busca Y Elimina El Encontrado:
        public async Task<int> EliminarZapato(Zapato zapato)
        {
            Zapato? Objeto_Obtenido = await _MyDBcontext.Zapatos.FirstOrDefaultAsync(x => x.Id_Zapato == zapato.Id_Zapato);

            if (Objeto_Obtenido != null)
            {
                _MyDBcontext.Remove(Objeto_Obtenido);
            }

            return await _MyDBcontext.SaveChangesAsync();
        }
    }
}
