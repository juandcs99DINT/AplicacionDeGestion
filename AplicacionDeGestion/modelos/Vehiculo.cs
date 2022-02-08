using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.modelos
{
    public class Vehiculo : ObservableObject
    {
        public Vehiculo() { }

        public Vehiculo(int idVehiculo, int idCliente, string matricula, int idMarca, string modelo, string tipo)
        {
            this.idVehiculo = idVehiculo;
            this.idCliente = idCliente;
            this.matricula = matricula;
            this.idMarca = idMarca;
            this.modelo = modelo;
            this.tipo = tipo;
        }

        public Vehiculo(Vehiculo vehiculo)
        {
            this.idVehiculo = vehiculo.idVehiculo;
            this.idCliente = vehiculo.idCliente;
            this.matricula = vehiculo.matricula;
            this.idMarca = vehiculo.idMarca;
            this.modelo = vehiculo.modelo;
            this.tipo = vehiculo.tipo;
        }

        private int idVehiculo;
        public int IdVehiculo
        {
            get => idVehiculo;
            set => SetProperty(ref idVehiculo, value);
        }

        private int? idCliente;
        public int? IdCliente
        {
            get => idCliente;
            set => SetProperty(ref idCliente, value);
        }

        private string matricula;
        public string Matricula
        {
            get => matricula;
            set => SetProperty(ref matricula, value);
        }

        private int? idMarca;
        public int? IdMarca
        {
            get => idMarca;
            set => SetProperty(ref idMarca, value);
        }

        private string modelo;
        public string Modelo
        {
            get => modelo;
            set => SetProperty(ref modelo, value);
        }

        private string tipo;
        public string Tipo
        {
            get => tipo;
            set => SetProperty(ref tipo, value);
        }
    }
}
