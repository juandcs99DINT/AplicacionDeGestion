using AplicacionDeGestion.modelos;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.servicios
{
    public class MarcaAñadidaMessage : ValueChangedMessage<string>
    {
        public MarcaAñadidaMessage(string marca) : base(marca) { }
    }

    internal class NuevoClienteMessage : ValueChangedMessage<Cliente>
    {
        public NuevoClienteMessage(Cliente nuevoCliente) : base(nuevoCliente) { }
    }

    internal class ClienteSeleccionadoMessage : RequestMessage<Cliente> { }
}
