﻿using AplicacionDeGestion.viewmodels;
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
    /// Lógica de interacción para FinalizacionEstacionamientoDialogo.xaml
    /// </summary>
    public partial class FinalizacionEstacionamientoDialogo : Window
    {
        private readonly FinalizacionEstacionamientoVM vm;
        public FinalizacionEstacionamientoDialogo()
        {
            vm = new FinalizacionEstacionamientoVM();
            this.DataContext = vm;
            InitializeComponent();
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e) => DialogResult = true;
    }
}
