using AplicacionDeGestion.modelos;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.viewmodels
{
    class UsuarioFormularioVM : ObservableObject
    {

        private Cliente usuarioFormulario;
        public UsuarioFormularioVM() { }

        public Cliente UsuarioFormulario
        {
            get { return usuarioFormulario; }
            set { SetProperty(ref usuarioFormulario, value); }
        }


    }
}
