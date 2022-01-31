using AplicacionDeGestion.viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AplicacionDeGestion.vistas
{
    /// <summary>
    /// Lógica de interacción para CrearModificarVehiculoFormulario.xaml
    /// </summary>
    public partial class CrearModificarVehiculoFormulario : Window
    {
        private readonly VehiculoFormularioVM vm;
        public CrearModificarVehiculoFormulario()
        {
            vm = new VehiculoFormularioVM();
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
