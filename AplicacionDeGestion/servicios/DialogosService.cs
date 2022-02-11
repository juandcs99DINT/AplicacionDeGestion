using Microsoft.Win32;
using System;
using System.Windows;

namespace AplicacionDeGestion.servicios
{
    class DialogosService
    {
        public DialogosService() { }

        /// <summary>
        /// Diálogo que devuelve la ruta del fichero local que seleccionemos.
        /// </summary>
        /// <returns>Ruta del fichero local</returns>
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

        /// <summary>
        /// Diálogo génerico que pregunta al usuario si realmente desea confirmar su acción.
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar al usuario</param>
        /// <returns>Boolean en relación a si el usuario ha confirmado o cancelado su accion.</returns>
        public Boolean DialogoConfirmacionAccion(string mensaje)
        {
            MessageBoxResult result = MessageBox.Show(mensaje, "Parking", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        /// <summary>
        /// Diálogo similar al génerico, cambiando su icono al de peligro.
        /// </summary>
        /// <returns>Boolean en relación a si el usuario ha confirmado o cancelado su accion.</returns>
        public Boolean DialogoFinEstacionamiento()
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de terminar éste estacionamiento?", "Parking", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes) return true;
            else return false;
        }

        /// <summary>
        /// Diálogo génerico de error para mostrar en caso de fallo
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar al usuario</param>
        public void DialogoError(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Diálogo génerico de información
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar al usuario</param>
        public void DialogoInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Diálogo génerico de algún evento de peligro
        /// </summary>
        /// <param name="mensaje">Mensaje a mostrar al usuario</param>
        public void DialogoPrecaucion(string mensaje)
        {
            MessageBox.Show(mensaje, "Parking", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Manual de usuario con todas las instrucciones para utilizar el programa
        /// </summary>
        public void DialogoManualUsuario() => System.Windows.Forms.Help.ShowHelp(null, @"../../assets/manual_usuario/ManualUsuario.chm");

    }
}

