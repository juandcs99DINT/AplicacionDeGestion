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

        public UserControl CambiarAPestañaClientes() => new ClientesUserControl();
        public UserControl CambiarAPestañaVehiculos() => new VehiculosUserControl();
        public UserControl CambiarAPestañaEstacionamientos() => new EstacionamientosUserControl();

        public void AbrirDialogoAñadirMarca()
        {
            AñadirMarcaDialogo añadirMarcaDialogo = new AñadirMarcaDialogo();
            añadirMarcaDialogo.ShowDialog();
        }
        public void AbrirDialogoCrearModificarCliente()
        {
            CrearModificarClienteFormulario dialogoClienteFormulario = new CrearModificarClienteFormulario();
            dialogoClienteFormulario.ShowDialog();
        }
        public void AbrirDialogoCrearModificarVehiculo()
        {
            CrearModificarVehiculoFormulario dialogoVehiculoFormulario = new CrearModificarVehiculoFormulario();
            dialogoVehiculoFormulario.ShowDialog();
        }
        public void AbrirDialogoFinEstacionamiento()
        {
            FinalizacionEstacionamientoDialogo dialogoFinEstacionamiento = new FinalizacionEstacionamientoDialogo();
            dialogoFinEstacionamiento.ShowDialog();
        }

    }
}
