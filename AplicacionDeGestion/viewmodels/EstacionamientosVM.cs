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
    class EstacionamientosVM : ObservableObject
    {
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();
        public EstacionamientosVM()
        {
            EsperarCambioEnLaLista();
            RegistrarEnvioEstacionamientoSeleccionado();
            RefrescarLista();
            FinalizarEstacionamientoCommand = new RelayCommand(FinalizarEstacionamiento);
            DeseleccionarEstacionamientoCommand = new RelayCommand(DeseleccionarEstacionamiento);
            RefrescarListaCommand = new RelayCommand(RefrescarLista);
        }

        public RelayCommand FinalizarEstacionamientoCommand { get; }
        public RelayCommand DeseleccionarEstacionamientoCommand { get; }
        public RelayCommand RefrescarListaCommand { get; }

        private ObservableCollection<Estacionamiento> listaEstacionamientos;
        public ObservableCollection<Estacionamiento> ListaEstacionamientos
        {
            get => listaEstacionamientos;
            set => SetProperty(ref listaEstacionamientos, value);
        }

        private Estacionamiento estacionamientoSeleccionado;
        public Estacionamiento EstacionamientoSeleccionado
        {
            get => estacionamientoSeleccionado;
            set => SetProperty(ref estacionamientoSeleccionado, value);
        }

        private void RefrescarLista() => ListaEstacionamientos = datosService.GetEstacionamientos(true);
       
        private void EsperarCambioEnLaLista()
        {
            WeakReferenceMessenger.Default.Register<DatoAñadidoOModificadoMessage>(this, (r, m) =>
            {
                if (m.Value)
                {
                    ListaEstacionamientos = datosService.GetEstacionamientos(true);
                }
            });
        }
        private void RegistrarEnvioEstacionamientoSeleccionado()
        {
            WeakReferenceMessenger.Default.Register<EstacionamientosVM, EstacionamientoSeleccionadoMessage>
              (this, (r, m) =>
              {
                  m.Reply(r.EstacionamientoSeleccionado);
              });
        }

        private void FinalizarEstacionamiento() => navigationService.AbrirDialogoFinEstacionamiento();
        private void DeseleccionarEstacionamiento() => EstacionamientoSeleccionado = null;
    }
}
