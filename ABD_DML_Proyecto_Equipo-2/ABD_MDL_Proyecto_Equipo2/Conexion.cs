using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ABD_MDL_Proyecto_Equipo2
{
    class Conexion
    {
        private SqlConnection conexion = new SqlConnection(@"");    //Ingresa el String de tu conexion a SQL Server
        public SqlConnection AbrirConexion()    //Abrir conexion
        {
            conexion.Open();
            return conexion;
        }
        public SqlConnection CerrarConexion()   //Cerrar conexion
        {
            conexion.Close();
            return conexion;
        }
    }
}
