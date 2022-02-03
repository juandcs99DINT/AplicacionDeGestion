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
    class FinalizacionEstacionamientoVM : ObservableObject
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly DatosService datosService = new DatosService();

        public FinalizacionEstacionamientoVM()
        {
            RecibirEstacionamiento();
            CalcularImporteYSalida();
            AceptarCommand = new RelayCommand(FinalizarEstacionamiento);
        }

        public RelayCommand AceptarCommand { get; }
        public RelayCommand AñadirMarcaCommand { get; }

        private Estacionamiento estacionamiento;
        public Estacionamiento Estacionamiento
        {
            get => estacionamiento;
            set => SetProperty(ref estacionamiento, value);
        }

        // AP1 - PARA INICIAR UN ESTACIONAMIENTO ES Estacionamiento.Entrada = DateTime.Now.ToString()

        private void CalcularImporteYSalida()
        {
            DateTime entrada = Convert.ToDateTime(Estacionamiento.Entrada);
            DateTime salida = DateTime.Now;
            TimeSpan diferenciaFechas = salida - entrada;
            int tiempoDentroDelParking = (int)diferenciaFechas.TotalSeconds / 60;
            Vehiculo vehiculoCliente = datosService.GetVehiculoByMatricula(Estacionamiento.Matricula);
            float importe = Properties.Settings.Default.precioParking * tiempoDentroDelParking;
            if (vehiculoCliente != null)
            {
                dialogosService.DialogoInformacion("El cliente es abonado. Así que tiene un 10% de descuento.");
                importe *= 0.90f;
            }
            Estacionamiento.Importe = importe;
            Estacionamiento.Salida = salida.ToString();
        }

        private void FinalizarEstacionamiento()
        {
            bool datoCambiado = false;
            if (dialogosService.DialogoConfirmacionAccion($"¿Estás seguro de querer finalizar el estacionamiento?")
                && datosService.FinalizarEstacionamiento(Estacionamiento) > 0)
            {
                dialogosService.DialogoInformacion("Has finalizado el estacionamiento correctamente");
                datoCambiado = true;
            }
            else
            {
                dialogosService.DialogoError("No se ha podido finalizar el estacionamiento.");
            }
            WeakReferenceMessenger.Default.Send(new DatoAñadidoOModificadoMessage(datoCambiado));
        }

        public void RecibirEstacionamiento()
        {
            Estacionamiento = WeakReferenceMessenger.Default.Send<EstacionamientoSeleccionadoMessage>();
        }
    }
}
