using AplicacionDeGestion.modelos;
using AplicacionDeGestion.servicios;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.viewmodels
{
    class ClientesVM : ObservableObject
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();
        public ClientesVM()
        {
            ListaClientes = datosService.GetClientes();
            EsperarCambioEnLaLista();
            RegistrarEnvioClienteSeleccionado();
            AñadirClienteCommand = new RelayCommand(AñadirCliente);
            ModificarClienteCommand = new RelayCommand(ModificarCliente);
            EliminarClienteCommand = new RelayCommand(EliminarCliente);
            DeseleccionarClienteCommand = new RelayCommand(DeseleccionarCliente);
        }

        public RelayCommand DeseleccionarClienteCommand { get; }
        public RelayCommand AñadirClienteCommand { get; }
        public RelayCommand ModificarClienteCommand { get; }
        public RelayCommand EliminarClienteCommand { get; }

        private ObservableCollection<Cliente> listaClientes;
        public ObservableCollection<Cliente> ListaClientes
        {
            get => listaClientes;
            set => SetProperty(ref listaClientes, value);
        }

        private Cliente clienteSeleccionado;
        public Cliente ClienteSeleccionado
        {
            get => clienteSeleccionado;
            set => SetProperty(ref clienteSeleccionado, value);
        }

        private void DeseleccionarCliente() => ClienteSeleccionado = null;

        private void RegistrarEnvioClienteSeleccionado()
        {
            WeakReferenceMessenger.Default.Register<ClientesVM, ClienteSeleccionadoMessage>
              (this, (r, m) =>
              {
                  m.Reply(r.ClienteSeleccionado);
              });
        }

        private void EsperarCambioEnLaLista()
        {
            WeakReferenceMessenger.Default.Register<DatoAñadidoOModificadoMessage>(this, (r, m) =>
            {
                if (m.Value)
                {
                    ListaClientes = datosService.GetClientes();
                }
            });
        }

        private void AñadirCliente() => navigationService.AbrirDialogoCrearModificarCliente();
        private void ModificarCliente() => navigationService.AbrirDialogoCrearModificarCliente();

        private bool ComprobarParaEliminar()
        {
            ObservableCollection<Vehiculo> listaVehiculoCliente = datosService.GetVehiculosByIdCliente(ClienteSeleccionado.IdCliente);
            if (listaVehiculoCliente.Count > 0)
            {
                for (int i = 0; i < listaVehiculoCliente.Count; i++)
                {
                    if (datosService.GetEstacionamientoByMatricula(listaVehiculoCliente[i].Matricula) != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void EliminarCliente()
        {
            if(ComprobarParaEliminar())
            {
                if (dialogosService.DialogoConfirmacionAccion($"¿Estás seguro de querer eliminar el cliente {ClienteSeleccionado.Nombre} con DNI {ClienteSeleccionado.Documento}?") && datosService.EliminarCliente(ClienteSeleccionado) > 0)
                {
                    dialogosService.DialogoInformacion("Has eliminado el cliente correctamente");
                    ListaClientes = datosService.GetClientes();
                }
            }
            else
            {
                dialogosService.DialogoError("No se puede eliminar a un cliente con un estacionamiento activo.");
            }
        }
    }
}
