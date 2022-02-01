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

    internal class ClienteNuevoModificadoMessage : ValueChangedMessage<Cliente>
    {
        public ClienteNuevoModificadoMessage(Cliente cliente) : base(cliente) { }
    }

    internal class ClienteSeleccionadoMessage : RequestMessage<Cliente> { }
}
