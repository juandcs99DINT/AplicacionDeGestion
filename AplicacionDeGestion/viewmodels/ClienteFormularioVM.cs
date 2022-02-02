using AplicacionDeGestion.modelos;
using AplicacionDeGestion.servicios;
using Azure.Storage.Blobs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using NavigationService = AplicacionDeGestion.servicios.NavigationService;

namespace AplicacionDeGestion.viewmodels
{
    class ClienteFormularioVM : ObservableRecipient
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly AzureBlobStorageService azureBlobStorageService = new AzureBlobStorageService();
        private readonly AzureFaceService azureFaceService = new AzureFaceService();
        private readonly DatosService datosService = new DatosService();

        private Cliente cliente;

        public Cliente Cliente
        {
            get { return cliente; }
            set { SetProperty(ref cliente, value); }
        }

        private Boolean añadirNuevoCliente;
        public Boolean AñadirNuevoCliente
        {
            get => añadirNuevoCliente;
            set => SetProperty(ref añadirNuevoCliente, value);
        }

        public RelayCommand AceptarCommand { get; }
        public RelayCommand ExaminarImagenCommand { get; }

        public ClienteFormularioVM()
        {
            RecibirCliente();
            AceptarCommand = new RelayCommand(AñadirModificarCliente);
            ExaminarImagenCommand = new RelayCommand(ExaminarImagen);
        }

        private void AñadirModificarCliente()
        {
            if (añadirNuevoCliente)
            {
               
                    datosService.AñadirCliente(Cliente);

            }
            else
            {
                datosService.ModificarCliente(Cliente);
            }
            WeakReferenceMessenger.Default.Send(new ClienteNuevoModificadoMessage(Cliente));
        }

        public void RecibirCliente()
        {
            Cliente = WeakReferenceMessenger.Default.Send<ClienteSeleccionadoMessage>();
            if (Cliente == null)
            {
                AñadirNuevoCliente = true;
                Cliente = new Cliente();
            }
        }

        public void ExaminarImagen()
        {
            string rutaImagen = dialogosService.DialogoOpenFile();
            if (rutaImagen.Length != 0)
            {
                BlobContainerClient blobContainerClient = azureBlobStorageService.SubirImagenAzure(rutaImagen);
                string urlImagenAzure = azureBlobStorageService.ObtenerURLImagenAzure(blobContainerClient, rutaImagen);
                Cliente.Foto = urlImagenAzure;
                DeducirEdadYGenero(urlImagenAzure);
            }
        }

        public void DeducirEdadYGenero(string url)
        {
            FaceAttributes faceAttributes = azureFaceService.GetEdadGenero(url);
            Cliente.Genero = faceAttributes.gender == "female" ? "Mujer" : "Hombre";
            Cliente.Edad = Convert.ToInt32(faceAttributes.age);
        }
    }
}
