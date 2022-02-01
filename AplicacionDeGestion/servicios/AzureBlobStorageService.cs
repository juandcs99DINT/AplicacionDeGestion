using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.servicios
{
    class AzureBlobStorageService
    {
        private readonly DialogosService dialogosService = new DialogosService();
        public BlobContainerClient SubirImagenAzure(string rutaImagen)
        {
            string cadenaConexion = Properties.Settings.Default.cadenaConexionBlobStorage;
            string nombreContenedorBlobs = Properties.Settings.Default.nombreContenedorBlob;
            BlobContainerClient clienteContenedor = null;
            var clienteBlobService = new BlobServiceClient(cadenaConexion);
            try
            {
                clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorBlobs);

                Stream streamImagen = File.OpenRead(rutaImagen);
                string nombreImagen = Path.GetFileName(rutaImagen);

                clienteContenedor.DeleteBlobIfExists(nombreImagen);
                clienteContenedor.UploadBlob(nombreImagen, streamImagen);
            }
            catch (Exception)
            {
                dialogosService.DialogoError("Se ha producido un error al subir la imagen a Azure.");
            }
            return clienteContenedor;
        }

        public string ObtenerURLImagenAzure(BlobContainerClient clienteContenedor, string rutaImagen)
        {
            BlobClient clienteBlobImagen = null;
            string nombreImagen = Path.GetFileName(rutaImagen);
            try
            {
                clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
            }
            catch (Exception)
            {
                dialogosService.DialogoError("Se ha producido un error al recuperar la imagen de Azure.");
            }
            return clienteBlobImagen.Uri.AbsoluteUri;
        }
    }
}
