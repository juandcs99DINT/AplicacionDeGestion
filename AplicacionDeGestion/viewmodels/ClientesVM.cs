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
            RegistrarEnvioClienteSeleccionado();
            AñadirClienteCommand = new RelayCommand(AñadirCliente);
            ModificarClienteCommand = new RelayCommand(ModificarCliente);
            EliminarClienteCommand = new RelayCommand(EliminarCliente);
        }

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

        private void AñadirCliente() => navigationService.AbrirDialogoCrearModificarCliente();
        private void ModificarCliente() => navigationService.AbrirDialogoCrearModificarCliente();


        private void EliminarCliente()
        {



        }
    }
}
