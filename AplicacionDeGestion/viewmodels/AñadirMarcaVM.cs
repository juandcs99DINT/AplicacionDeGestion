using AplicacionDeGestion.servicios;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.viewmodels
{
    class AñadirMarcaVM : ObservableObject
    {
        private readonly DatosService datosService = new DatosService();
        public AñadirMarcaVM()
        {
            AceptarCommand = new RelayCommand(Aceptar);
        }

        public RelayCommand AceptarCommand { get; }

        private string marcaNueva;
        public string MarcaNueva
        {
            get => marcaNueva;
            set => SetProperty(ref marcaNueva, value);
        }
        public void Aceptar()
        {
            bool datoCambiado = datosService.AñadirMarca(MarcaNueva) > 0;
            WeakReferenceMessenger.Default.Send(new DatoAñadidoOModificadoMessage(datoCambiado));
        }
    }
}
