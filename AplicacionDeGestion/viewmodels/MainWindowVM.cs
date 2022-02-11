using AplicacionDeGestion.servicios;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AplicacionDeGestion.viewmodels
{
    class MainWindowVM : ObservableObject
    {
        private readonly NavigationService navigationService;
        private readonly DialogosService dialogosService;
        public MainWindowVM()
        {
            navigationService = new NavigationService();
            dialogosService = new DialogosService();
            UserControlActual = navigationService.CambiarAPestañaClientes();
            CambiarAClientesCommand = new RelayCommand(CambiarAClientes);
            CambiarAVehiculosCommand = new RelayCommand(CambiarAVehiculos);
            CambiarAEstacionamientosCommand = new RelayCommand(CambiarAEstacionamientos);
            AbrirManualUsuarioCommand = new RelayCommand(AbrirManualUsuario);
        }

        private UserControl userControlActual;
        public UserControl UserControlActual
        {
            get => userControlActual;
            set => SetProperty(ref userControlActual, value);
        }

        public RelayCommand CambiarAClientesCommand { get; }
        public RelayCommand CambiarAVehiculosCommand { get; }
        public RelayCommand CambiarAEstacionamientosCommand { get; }
        public RelayCommand AbrirManualUsuarioCommand { get; }

        public void AbrirManualUsuario() => dialogosService.DialogoManualUsuario();
        public void CambiarAClientes() => UserControlActual = navigationService.CambiarAPestañaClientes();
        public void CambiarAVehiculos() => UserControlActual = navigationService.CambiarAPestañaVehiculos();
        public void CambiarAEstacionamientos() => UserControlActual = navigationService.CambiarAPestañaEstacionamientos();
    }
}
