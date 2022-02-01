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
        private readonly NavigationService navegacion;
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly AzureBlobStorageService azureBlobStorageService = new AzureBlobStorageService();

        private Cliente cliente;

        public Cliente Cliente
        {
            get { return Cliente; }
            set { SetProperty(ref cliente, value); }
        }

        public RelayCommand AceptarCommand { get; }

        public ClienteFormularioVM() 
        {
            Cliente = WeakReferenceMessenger.Default.Send<ClienteSeleccionadoMessage>();
            navegacion = new NavigationService();
            Cliente = new Cliente();
            AceptarCommand = new RelayCommand(AñadirModificarCliente);
        }

        private void AñadirModificarCliente()
        {
            WeakReferenceMessenger.Default.Send(new NuevoClienteMessage(Cliente));
        }


        // He puesto ésto aquí para que ya lo tengas.
        public void ExaminarImagen()
        {
            string rutaImagen = dialogosService.DialogoOpenFile();
            if (rutaImagen.Length != 0)
            {
                BlobContainerClient blobContainerClient = azureBlobStorageService.SubirImagenAzure(rutaImagen);
                Cliente.Foto = azureBlobStorageService.ObtenerURLImagenAzure(blobContainerClient, rutaImagen);
            }
        }
    }
}
