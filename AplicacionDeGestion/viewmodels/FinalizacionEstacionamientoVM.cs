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
    class FinalizacionEstacionamientoVM : ObservableRecipient
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

        private Estacionamiento estacionamientoSeleccionado;
        public Estacionamiento EstacionamientoSeleccionado
        {
            get => estacionamientoSeleccionado;
            set => SetProperty(ref estacionamientoSeleccionado, value);
        }

        // AP1 - PARA INICIAR UN ESTACIONAMIENTO ES Estacionamiento.Entrada = DateTime.Now.ToString()

        private void CalcularImporteYSalida()
        {
            DateTime entrada = Convert.ToDateTime(EstacionamientoSeleccionado.Entrada);
            DateTime salida = DateTime.Now;
            TimeSpan diferenciaFechas = salida - entrada;
            int tiempoDentroDelParking = (int)diferenciaFechas.TotalSeconds / 60;
            Vehiculo vehiculoCliente = datosService.GetVehiculoByMatricula(EstacionamientoSeleccionado.Matricula);
            float importe = (float)Math.Round(Properties.Settings.Default.precioParking * tiempoDentroDelParking, 2);
            if (vehiculoCliente != null)
            {
                dialogosService.DialogoInformacion("El cliente es abonado. Así que tiene un 10% de descuento.");
                importe *= 0.90f;
            }
            EstacionamientoSeleccionado.Importe = importe;
            EstacionamientoSeleccionado.Salida = salida.ToString();
        }

        private void FinalizarEstacionamiento()
        {
            bool datoCambiado = false;
            if (dialogosService.DialogoConfirmacionAccion($"¿Estás seguro de querer finalizar el estacionamiento?")
                && datosService.FinalizarEstacionamiento(EstacionamientoSeleccionado) > 0)
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
            EstacionamientoSeleccionado = WeakReferenceMessenger.Default.Send<EstacionamientoSeleccionadoMessage>();
        }
    }
}
