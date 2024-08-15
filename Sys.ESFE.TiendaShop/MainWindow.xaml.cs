using CapaBL;
using CapaEN;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Sys.ESFE.TiendaShop
{
    public partial class MainWindow : Window
    {
        private ProductosBL productosBL;
        private ProductosEN productoSeleccionado;

        public MainWindow()
        {
            InitializeComponent();
            productosBL = new ProductosBL();
            CargarDatos();
        }

        private void CargarDatos()
        {
            // Obtener la lista de productos desde la capa de negocio
            List<ProductosEN> productos = productosBL.ObtenerProductos();
            // Asignar la lista al DataGrid
            dgProductos.ItemsSource = productos;
        }

        private void dgProductos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Obtener el producto seleccionado
            productoSeleccionado = dgProductos.SelectedItem as ProductosEN;

            if (productoSeleccionado != null)
            {
                // Cargar los datos del producto en los campos de texto
                txtNombre.Text = productoSeleccionado.Nombre;
                txtPrecioUnitario.Text = productoSeleccionado.PrecioUnitario.ToString();
                txtId.Text = productoSeleccionado.Id.ToString(); // Asegúrate de que txtId esté visible si es necesario
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validar que el campo Nombre no esté vacío y que PrecioUnitario sea un número válido
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    !decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario))
                {
                    MessageBox.Show("Por favor, ingrese un nombre y un precio unitario válidos.");
                    return;
                }

                // Crear una instancia de la clase ProductosEN
                ProductosEN nuevoProducto = new ProductosEN
                {
                    Nombre = txtNombre.Text,
                    PrecioUnitario = precioUnitario
                };

                // Llamar al método para guardar el producto
                int resultado = productosBL.GuardarProductos(nuevoProducto);

                // Comprobar si la operación fue exitosa
                if (resultado > 0)
                {
                    MessageBox.Show("Producto guardado exitosamente.");
                    CargarDatos(); // Recargar los datos para mostrar el nuevo producto
                    LimpiarCampos(); // Limpiar los campos de texto
                }
                else
                {
                    MessageBox.Show("Error al guardar el producto.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado != null)
            {
                try
                {
                    // Validar que el campo Nombre no esté vacío y que PrecioUnitario sea un número válido
                    if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                        !decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario) ||
                        !int.TryParse(txtId.Text, out int id))
                    {
                        MessageBox.Show("Por favor, ingrese un ID, nombre y un precio unitario válidos.");
                        return;
                    }

                    // Actualizar los datos del producto con los valores editados
                    productoSeleccionado.Id = id;
                    productoSeleccionado.Nombre = txtNombre.Text;
                    productoSeleccionado.PrecioUnitario = precioUnitario;

                    // Llamar al método para modificar el producto
                    int resultado = productosBL.ModificarProductos(productoSeleccionado);

                    // Comprobar si la operación fue exitosa
                    if (resultado > 0)
                    {
                        MessageBox.Show("Producto modificado exitosamente.");
                        CargarDatos(); // Recargar los datos para mostrar los cambios
                        LimpiarCampos(); // Limpiar los campos de texto
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar el producto.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para editar.");
            }
        }

        private void LimpiarCampos()
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtPrecioUnitario.Text = "";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (productoSeleccionado != null)
            {
                try
                {
                    // Confirmar la acción de eliminación con el usuario
                    MessageBoxResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este producto?", "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (resultado == MessageBoxResult.Yes)
                    {
                        // Llamar al método para eliminar el producto
                        int resultadoOperacion = productosBL.EliminarProductos(productoSeleccionado);

                        // Comprobar si la operación fue exitosa
                        if (resultadoOperacion > 0)
                        {
                            MessageBox.Show("Producto eliminado exitosamente.");
                            CargarDatos(); // Recargar los datos para actualizar la lista
                            LimpiarCampos(); // Limpiar los campos de texto
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar el producto.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto para eliminar.");
            }
        }
    }
}
