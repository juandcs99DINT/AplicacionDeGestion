using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.modelos
{
    class FaceAttributes : ObservableObject
    {
        public FaceAttributes() { }
        public FaceAttributes(int edad, string genero)
        {
            this.edad = edad;
            this.genero = genero;
        }

        private int edad;
        public int Edad
        {
            get => edad;
            set => SetProperty(ref edad, value);
        }

        private string genero;
        public string Genero
        {
            get => genero;
            set => SetProperty(ref genero, value);
        }
    }
}
