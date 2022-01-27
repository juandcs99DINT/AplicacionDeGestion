using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.modelos
{
    class Estacionamiento : ObservableObject
    {
        public Estacionamiento() { }

        public Estacionamiento(int idEstacionamiento, int idVehiculo, string matricula, string entrada, string salida, float importe, string tipo)
        {
            this.idEstacionamiento = idEstacionamiento;
            this.idVehiculo = idVehiculo;
            this.matricula = matricula;
            this.entrada = entrada;
            this.salida = salida;
            this.importe = importe;
            this.tipo = tipo;
        }

        private int idEstacionamiento;
        public int IdEstacionamiento
        {
            get => idEstacionamiento;
            set => SetProperty(ref idEstacionamiento, value);
        }

        private int idVehiculo;
        public int IdVehiculo
        {
            get => idVehiculo;
            set => SetProperty(ref idVehiculo, value);
        }

        private string matricula;
        public string Matricula
        {
            get => matricula;
            set => SetProperty(ref matricula, value);
        }

        private string entrada;
        public string Entrada
        {
            get => entrada;
            set => SetProperty(ref entrada, value);
        }

        private string salida;
        public string Salida
        {
            get => salida;
            set => SetProperty(ref salida, value);
        }

        private float importe;
        public float Importe
        {
            get => importe;
            set => SetProperty(ref importe, value);
        }

        private string tipo;
        public string Tipo
        {
            get => tipo;
            set => SetProperty(ref tipo, value);
        }
    }
}
