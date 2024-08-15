using CapaDAL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaBL
{
    public class ProductosBL
    {
        public ProductosDAL ProductosDAL = new ProductosDAL();

        public int GuardarProductos(ProductosEN pProductosEN)
        {
            return ProductosDAL.GuardarProductos(pProductosEN);
        }

        public int EliminarProductos(ProductosEN pProductosEN)
        {
            return ProductosDAL.EliminarProductos(pProductosEN);
        }

        public int ModificarProductos(ProductosEN pProductosEN)
        {
            return ProductosDAL.ModificarProductos(pProductosEN);
        }

        public List<ProductosEN> ObtenerProductos()
        {
            return ProductosDAL.ObtenerProductos();
        }
    }
}
