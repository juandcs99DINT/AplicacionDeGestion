using AplicacionDeGestion.vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AplicacionDeGestion.servicios
{
    class NavigationService
    {
        public NavigationService() { }

        private static readonly UserControl pestañaClientes = new ClientesUserControl();
        private static readonly UserControl pestañaVehiculos = new VehiculosUserControl();
        private static readonly UserControl pestañaEstacionamientos = new EstacionamientosUserControl();

        /// <summary>
        /// Cambia el UserControlActual a Clientes.
        /// </summary>
        /// <returns>UserControl</returns>
        public UserControl CambiarAPestañaClientes() => pestañaClientes;
        /// <summary>
        /// Cambia el UserControlActual a Vehiculos.
        /// </summary>
        /// <returns>UserControl</returns>
        public UserControl CambiarAPestañaVehiculos() => pestañaVehiculos;
        /// <summary>
        /// Cambia el UserControlActual a Estacionamientos.
        /// </summary>
        /// <returns>UserControl</returns>
        public UserControl CambiarAPestañaEstacionamientos() => pestañaEstacionamientos;

        /// <summary>
        /// Abre el diálogo para añadir una nueva marca.
        /// </summary>
        public void AbrirDialogoAñadirMarca()
        {
            AñadirMarcaDialogo añadirMarcaDialogo = new AñadirMarcaDialogo();
            añadirMarcaDialogo.ShowDialog();
        }
        /// <summary>
        /// Abre el formulario para añadir o modificar un cliente.
        /// </summary>
        public void AbrirDialogoCrearModificarCliente()
        {
            CrearModificarClienteFormulario dialogoClienteFormulario = new CrearModificarClienteFormulario();
            dialogoClienteFormulario.ShowDialog();
        }
        /// <summary>
        ///  Abre el formulario para añadir o modificar un vehículo.
        /// </summary>
        public void AbrirDialogoCrearModificarVehiculo()
        {
            CrearModificarVehiculoFormulario dialogoVehiculoFormulario = new CrearModificarVehiculoFormulario();
            dialogoVehiculoFormulario.ShowDialog();
        }
        /// <summary>
        /// Abre el diálogo para finalizar un estacionamiento seleccionado.
        /// </summary>
        public void AbrirDialogoFinEstacionamiento()
        {
            FinalizacionEstacionamientoDialogo dialogoFinEstacionamiento = new FinalizacionEstacionamientoDialogo();
            dialogoFinEstacionamiento.ShowDialog();
        }

    }
}
