using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class ComunBD
    {
        //Aqui se debe ingresar la cadena de conexion de la base de datos 
        const string StrConexion = @"Data Source=localhost;Initial Catalog=SchoolShop;Integrated Security=True;TrustServerCertificate=True";

        private static SqlConnection ObtenerConexion()
        {
            //--- conn = connection
            SqlConnection Comun = new SqlConnection(StrConexion);
            Comun.Open();
            return Comun;
        }

        public static SqlCommand ObtenerComando()
        {
            //--- cmd =  Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = ObtenerConexion();
            return cmd;
        }
        public static int EjecutarComando(SqlCommand pComando)
        {
            int resultado = pComando.ExecuteNonQuery();
            pComando.Connection.Close();
            return resultado;
        }

        public static SqlDataReader EjecutarComandoReader(SqlCommand pComando)
        {
            SqlDataReader reader = pComando.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
