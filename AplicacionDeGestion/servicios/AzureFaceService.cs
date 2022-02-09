﻿using AplicacionDeGestion.modelos;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    class AzureFaceService
    {
        private readonly DialogosService dialogosService = new DialogosService();
        /// <summary>
        /// Devuelve una clase con los atributos faciales en base a la imagen de una persona.
        /// </summary>
        /// <param name="url">URL de la imagen alojada en Azure.</param>
        /// <returns>Clase que contiene los atributos faciales (género y edad).</returns>
        public FaceAttributes GetEdadGenero(string url)
        {
            FaceAttributes faceAttributes = new FaceAttributes();
            try
            {
                JObject requestBody = new JObject();
                requestBody.Add(new JProperty("url", url));
                var client = new RestClient(Properties.Settings.Default.endpointFace);
                var request = new RestRequest("face/v1.0/detect", Method.POST);
                request.AddHeader("Ocp-Apim-Subscription-Key", Properties.Settings.Default.keyFace);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(requestBody), ParameterType.RequestBody);
                request.AddParameter("returnFaceAttributes", "age,gender", ParameterType.QueryString);
                var response = client.Execute(request);
                List<Root> listaRoots = JsonConvert.DeserializeObject<List<Root>>(response.Content);
                faceAttributes = listaRoots[0].faceAttributes;
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return faceAttributes;
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

