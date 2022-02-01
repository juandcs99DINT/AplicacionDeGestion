using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;


namespace AplicacionDeGestion.modelos
{
    class Cliente : ObservableObject
    {
        public Cliente() { }

        public Cliente(int idCliente, string nombre, string documento, string foto, int edad, string genero, string telefono)
        {
            this.idCliente = idCliente;
            this.nombre = nombre;
            this.documento = documento;
            this.foto = foto;
            this.edad = edad;
            this.genero = genero;
            this.telefono = telefono;
        }

        private int idCliente;
        public int IdCliente
        {
            get => idCliente;
            set => SetProperty(ref idCliente, value);
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set => SetProperty(ref nombre, value);
        }

        private string documento;
        public string Documento
        {
            get => documento;
            set => SetProperty(ref documento, value);
        }

        private string foto;
        public string Foto
        {
            get => foto;
            set => SetProperty(ref foto, value);
        }

        private int? edad;
        public int? Edad
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

        private string telefono;

        public string Telefono
        {
            get => telefono;
            set => SetProperty(ref telefono, value);
        }
    }
}
