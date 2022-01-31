using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplicacionDeGestion.servicios
{
    class DialogosService
    {
        public DialogosService() { }

        public string DialogoOpenFile()
        {
            string fileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
            }
            return fileName;
        }

        public Boolean DialogoConfirmacionAccion(string mensaje)
        {
            MessageBoxResult result = MessageBox.Show(mensaje, "Parking", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        public Boolean DialogoFinEstacionamiento()
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de terminar éste estacionamiento?", "Parking", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        public void DialogoError(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void DialogoInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void DialogoPrecaucion(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


    }
}

