using AplicacionDeGestion.modelos;
using AplicacionDeGestion.servicios;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.viewmodels
{
    class ClientesVM : ObservableObject
    {
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly DatosService datosService = new DatosService();
        private readonly NavigationService navigationService = new NavigationService();
        public ClientesVM()
        {
            datosService.GetClientes();
        }

        private ObservableCollection<Cliente> listaClientes;
        public ObservableCollection<Cliente> ListaClientes
        {
            get => listaClientes;
            set => SetProperty(ref listaClientes, value);
        }
    }
}
