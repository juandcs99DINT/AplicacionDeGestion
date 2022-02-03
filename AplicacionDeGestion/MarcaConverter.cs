using AplicacionDeGestion.modelos;
using AplicacionDeGestion.servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AplicacionDeGestion
{
    class MarcaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DatosService datosService = new DatosService();
            int valor = Int32.Parse(value.ToString());
            return datosService.GetMarcaById(valor).Nombre;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
