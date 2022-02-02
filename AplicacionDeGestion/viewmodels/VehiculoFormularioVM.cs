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
    class VehiculoFormularioVM : ObservableObject
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly AzureBlobStorageService azureBlobStorageService = new AzureBlobStorageService();
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();

        public VehiculoFormularioVM()
        {
            RecibirVehiculo();
            AceptarCommand = new RelayCommand(AñadirModificarCliente);
            ExaminarVehiculoCommand = new RelayCommand(ExaminarImagen);
            AñadirMarcaCommand = new RelayCommand(AbrirVentanaAñadirMarca);
        }

        public RelayCommand AceptarCommand { get; }
        public RelayCommand ExaminarVehiculoCommand { get; }
        public RelayCommand AñadirMarcaCommand { get; }

        private Vehiculo vehiculo;
        public Vehiculo Vehiculo
        {
            get => vehiculo;
            set => SetProperty(ref vehiculo, value);
        }

        private ObservableCollection<string> listaMarcas;
        public ObservableCollection<string> ListaMarcas
        {
            get => listaMarcas;
            set => SetProperty(ref listaMarcas, value);
        }

        private Boolean añadirNuevoVehiculo;
        public Boolean AñadirNuevoVehiculo
        {
            get => añadirNuevoVehiculo;
            set => SetProperty(ref añadirNuevoVehiculo, value);
        }

        private void AñadirModificarCliente()
        {
            bool datoCambiado = false;
            if (AñadirNuevoVehiculo)
            {
                if (datosService.GetVehiculoByMatricula(Vehiculo.Matricula) == null)
                {
                    datoCambiado = datosService.AñadirVehiculo(Vehiculo) > 0;
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
            Vehiculo = WeakReferenceMessenger.Default.Send<VehiculoSeleccionadoMessage>();
            if (Vehiculo == null)
            {
                AñadirNuevoVehiculo = true;
                Vehiculo = new Vehiculo();
            }
        }

        public void ExaminarImagen()
        {
            string rutaImagen = dialogosService.DialogoOpenFile();
            if (rutaImagen.Length != 0)
            {
                BlobContainerClient blobContainerClient = azureBlobStorageService.SubirImagenAzure(rutaImagen);
                string urlImagenAzure = azureBlobStorageService.ObtenerURLImagenAzure(blobContainerClient, rutaImagen);
            }
        }

        public void AbrirVentanaAñadirMarca() => navigationService.AbrirDialogoAñadirMarca();
        public void RegistrarNacionalidadNueva()
        {
            WeakReferenceMessenger.Default.Register<MarcaAñadidaMessage>(this, (r, m) =>
            {
                ListaMarcas.Add(m.Value);
            });
        }

    }
}

