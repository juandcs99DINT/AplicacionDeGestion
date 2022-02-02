using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.modelos
{
    class Marca : ObservableObject
    {
        public Marca() { }

        public Marca(int idMarca, string nombre)
        {
            this.idMarca = idMarca;
            this.nombre = nombre;
        }

        private int idMarca;
        public int IdMarca
        {
            get => idMarca;
            set => SetProperty(ref idMarca, value);
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }
    }
}
