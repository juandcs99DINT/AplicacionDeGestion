using AplicacionDeGestion.modelos;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace AplicacionDeGestion.servicios
{
    class AzureServices
    {
        // Aquí va lo del Servicio Face y lo del Blob Storage
        // Lo del Custom Vision y Computer Vision va en la otra aplicación
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

        /*
           public void ExaminarImagen()
        {
            string rutaImagen = dialogosService.DialogoOpenFile();
            if (rutaImagen.Length != 0)
            {
                BlobContainerClient blobContainerClient = azureBlobStorageService.SubirImagenAzure(rutaImagen);
                Cliente.Foto = azureBlobStorageService.ObtenerURLImagenAzure(blobContainerClient, rutaImagen);               
            }
        }
        */
        public FaceAttributes GetEdadGenero(string url)
        {
            var client = new RestClient(Properties.Settings.Default.endpointFace);
            var request = new RestRequest("face/v1.0/detect", Method.POST);
            request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.keyFace);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(url), ParameterType.RequestBody);
            request.AddParameter("returnFaceAttributes", "age,gender", ParameterType.QueryString);
            var response = client.Execute(request);
            List<Root> listaRoots = JsonConvert.DeserializeObject<List<Root>>(response.Content);
            return listaRoots[0].faceAttributes;
        }
    }
    // CLASES PARA DESERIALIZAR // SERVICIO FACE
    public class FaceRectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class FaceAttributes
    {
        public string gender { get; set; }
        public double age { get; set; }
    }

    public class Root
    {
        public string faceId { get; set; }
        public FaceRectangle faceRectangle { get; set; }
        public FaceAttributes faceAttributes { get; set; }
    }
}

