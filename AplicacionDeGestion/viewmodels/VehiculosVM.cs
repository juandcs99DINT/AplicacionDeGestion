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
    class VehiculosVM : ObservableObject
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();
        public VehiculosVM()
        {
            ListaVehiculos = datosService.GetVehiculos();
            EsperarCambioEnLaLista();
            RegistrarEnvioVehiculoSeleccionado();
            AñadirVehiculoCommand = new RelayCommand(AñadirVehiculo);
            ModificarVehiculoCommand = new RelayCommand(ModificarVehiculo);
            EliminarVehiculoCommand = new RelayCommand(EliminarVehiculo);
            DeseleccionarVehiculoCommand = new RelayCommand(DeseleccionarVehiculo);
        }

        public RelayCommand DeseleccionarVehiculoCommand { get; }
        public RelayCommand AñadirVehiculoCommand { get; }
        public RelayCommand ModificarVehiculoCommand { get; }
        public RelayCommand EliminarVehiculoCommand { get; }

        private ObservableCollection<Vehiculo> listaVehiculos;
        public ObservableCollection<Vehiculo> ListaVehiculos
        {
            get => listaVehiculos;
            set => SetProperty(ref listaVehiculos, value);
        }

        private Vehiculo vehiculoSeleccionado;
        public Vehiculo VehiculoSeleccionado
        {
            get => vehiculoSeleccionado;
            set => SetProperty(ref vehiculoSeleccionado, value);
        }

        private void DeseleccionarVehiculo() => VehiculoSeleccionado = null;

        private void RegistrarEnvioVehiculoSeleccionado()
        {
            WeakReferenceMessenger.Default.Register<VehiculosVM, VehiculoSeleccionadoMessage>
              (this, (r, m) =>
              {
                  m.Reply(r.VehiculoSeleccionado);
              });
        }

        private void EsperarCambioEnLaLista()
        {
            WeakReferenceMessenger.Default.Register<DatoAñadidoOModificadoMessage>(this, (r, m) =>
            {
                if (m.Value)
                {
                    ListaVehiculos = datosService.GetVehiculos();
                }
            });
        }

        private void AñadirVehiculo() => navigationService.AbrirDialogoCrearModificarVehiculo();
        private void ModificarVehiculo() => navigationService.AbrirDialogoCrearModificarVehiculo();

        private void EliminarVehiculo()
        {
            if (dialogosService.DialogoConfirmacionAccion($"¿Estás seguro de querer eliminar el vehiculo" +
                $"con matrícula {VehiculoSeleccionado.Matricula} y perteneciente al cliente número {VehiculoSeleccionado.IdCliente}?"))
            {
                datosService.EliminarVehiculo(VehiculoSeleccionado);
                dialogosService.DialogoInformacion("Has eliminado el vehículo correctamente");
                ListaVehiculos = datosService.GetVehiculos();
            }
        }
    }
}

