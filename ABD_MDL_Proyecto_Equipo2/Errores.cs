using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ABD_MDL_Proyecto_Equipo2
{
    class Errores
    {

        public void cat(SqlException c)
        {
            SqlException e = c;
            if (e.Number == 208)
            {
                Console.WriteLine("Nombre de Tabla no existe");
                return;
            }
            if (e.Number == 207)
            {
                Console.WriteLine("Nombre de Columna no existe");
                return;
            }
            if (e.Number == 102)
            {
                Console.WriteLine("Error de Sintaxis, verifique su entrada" + "\r\n" + "Ejemplo: insertar en <tabla> columna(s) valores ('valor1', ...)  ");

                return;
            }
            if (e.Number == 245)
            {
                Console.WriteLine("\r\n  Algun valor introducido no es del tipo correcto; mas detalles: \r\n ");

                Console.WriteLine(e);

                return;
            }

            else
            {

                Console.WriteLine("Error en operacion SQL" + e);
            }

        }


    }
}
