using AplicacionDeGestion.modelos;
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
    class ClienteFormularioVM : ObservableObject
    {
        public ClienteFormularioVM() 
        {
            AceptarCommand = new RelayCommand(Aceptar);
        }

        public RelayCommand AceptarCommand { get; }
        private Cliente usuarioFormulario;
        public Cliente UsuarioFormulario
        {
            get => usuarioFormulario;
            set => SetProperty(ref usuarioFormulario, value);
        }

        public void Aceptar() => WeakReferenceMessenger.Default.Send(new ClienteAñadidoMessage(usuarioFormulario));
    }
}
