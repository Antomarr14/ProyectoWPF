using CapaEN;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDAL
{
    public class ProductosDAL
    {
        public int GuardarProductos(ProductosEN pProductosEN)
        {
            SqlCommand cmd = ComunBD.ObtenerComando();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPGuardarProductos";
            cmd.Parameters.AddWithValue("@Nombre", pProductosEN.Nombre);
            cmd.Parameters.AddWithValue("@PrecioUnitario", pProductosEN.PrecioUnitario);
            return ComunBD.EjecutarComando(cmd);
        }
        public int EliminarProductos(ProductosEN pProductosEN)
        {
            SqlCommand cmd = ComunBD.ObtenerComando();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPEliminarProductos";
            cmd.Parameters.AddWithValue("Id", pProductosEN.Id);
            return ComunBD.EjecutarComando(cmd);
        }
        public int ModificarProductos(ProductosEN pProductosEN)
        {
            SqlCommand cmd = ComunBD.ObtenerComando();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPModificarProductos";
            cmd.Parameters.AddWithValue("@Nombre", pProductosEN.Nombre);
            cmd.Parameters.AddWithValue("@PrecioUnitario", pProductosEN.PrecioUnitario);
            cmd.Parameters.AddWithValue("@Id", pProductosEN.Id);
            return ComunBD.EjecutarComando(cmd);
        }
        public List<ProductosEN> ObtenerProductos()
        {
            List<ProductosEN> lista = new List<ProductosEN>();
            SqlCommand cmd = ComunBD.ObtenerComando();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SPMostrarProductos";
            SqlDataReader reader = ComunBD.EjecutarComandoReader(cmd);
            while (reader.Read())
            {
                ProductosEN ProductosEN = new ProductosEN();
                ProductosEN.Id = reader.GetInt32(0);
                ProductosEN.Nombre = reader.GetString(1);
                ProductosEN.PrecioUnitario = reader.GetDecimal(2);
                lista.Add(ProductosEN);
            }
            return lista;

        }
    }
}
