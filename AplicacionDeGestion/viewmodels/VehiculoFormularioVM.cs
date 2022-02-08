using AplicacionDeGestion.modelos;
using AplicacionDeGestion.servicios;
using Azure.Storage.Blobs;
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
    class VehiculoFormularioVM : ObservableRecipient
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();

        public VehiculoFormularioVM()
        {
            RecibirVehiculo();
            RecibirCliente();
            RegistrarMarcaNueva();
            ListaMarcas = datosService.GetMarcas();
            ListaTipoVehiculos = datosService.GetTipoVehiculos();
            AceptarCommand = new RelayCommand(AñadirModificarVehiculo);
            AñadirMarcaCommand = new RelayCommand(AbrirVentanaAñadirMarca);
        }

        public RelayCommand AceptarCommand { get; }
        public RelayCommand AñadirMarcaCommand { get; }

        private Vehiculo vehiculo;
        public Vehiculo Vehiculo
        {
            get => vehiculo;
            set => SetProperty(ref vehiculo, value);
        }

        private ObservableCollection<Marca> listaMarcas;
        public ObservableCollection<Marca> ListaMarcas
        {
            get => listaMarcas;
            set => SetProperty(ref listaMarcas, value);
        }

        private ObservableCollection<string> listaTipoVehiculos;
        public ObservableCollection<string> ListaTipoVehiculos
        {
            get => listaTipoVehiculos;
            set => SetProperty(ref listaTipoVehiculos, value);
        }

        private bool añadirNuevoVehiculo;
        public bool AñadirNuevoVehiculo
        {
            get => añadirNuevoVehiculo;
            set => SetProperty(ref añadirNuevoVehiculo, value);
        }

        private Cliente cliente;
        public Cliente Cliente
        {
            get => cliente;
            set => SetProperty(ref cliente, value);
        }

        private void AñadirModificarVehiculo()
        {
            bool datoCambiado = false;
            if (AñadirNuevoVehiculo)
            {
                if (datosService.GetVehiculoByMatricula(Vehiculo.Matricula) == null)
                {
                    if(datosService.GetClienteById(Vehiculo.IdCliente) != null)
                    {
                        datoCambiado = datosService.AñadirVehiculo(Vehiculo) > 0;
                    } else
                    {
                        dialogosService.DialogoError("El ID del cliente no existe. El vehículo tiene que ir asociado a uno.");
                    }
                }
                else
                {
                    dialogosService.DialogoError("Ya existe un vehículo con esa matrícula. No se ha añadido el vehículo nuevo.");
                }
            }
            else
            {
                datoCambiado = datosService.ModificarVehiculo(Vehiculo) > 0;
            }
            WeakReferenceMessenger.Default.Send(new DatoAñadidoOModificadoMessage(datoCambiado));
        }

        public void RecibirVehiculo()
        {
            Vehiculo = new Vehiculo(WeakReferenceMessenger.Default.Send<VehiculoSeleccionadoMessage>());
            if (Vehiculo == null)
            {
                AñadirNuevoVehiculo = true;
                Vehiculo = new Vehiculo();
            }
        }

        public void RecibirCliente()
        {
            Cliente = WeakReferenceMessenger.Default.Send<ClienteSeleccionadoMessage>();
            if (Cliente != null)
            {
                Vehiculo.IdCliente = Cliente.IdCliente;
            }
        }

        public void AbrirVentanaAñadirMarca() => navigationService.AbrirDialogoAñadirMarca();
        public void RegistrarMarcaNueva()
        {
            WeakReferenceMessenger.Default.Register<DatoAñadidoOModificadoMessage>(this, (r, m) =>
            {
                if (m.Value)
                {
                    ListaMarcas = datosService.GetMarcas();
                }
            });
        }
    }
}


