using AplicacionDeGestion.modelos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionDeGestion.servicios
{
    class DatosService
    {
        private readonly SqliteConnection conexion = new SqliteConnection("Data Source=" + Properties.Settings.Default.rutaConexionBd + "/parking.db");

        // Para controlar que no haya dos clientes con el mismo documento.
        public Cliente GetClienteByDocument(string documento)
        {
            conexion.Open();
            Cliente cliente = null;
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes WHERE documento = @documento";
            SqliteDataReader cursorClientes = comando.ExecuteReader();

            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters["@documento"].Value = documento;

            if (cursorClientes.HasRows)
            {
                cursorClientes.Read();
                cliente = new Cliente(cursorClientes.GetInt32(0), (string)cursorClientes["nombre"], (string)cursorClientes["documento"],
                    (string)cursorClientes["foto"], cursorClientes.GetInt32(4), (string)cursorClientes["genero"], (string)cursorClientes["telefono"]);
            }
            cursorClientes.Close();
            conexion.Close();
            return cliente;
        }

        public ObservableCollection<Cliente> GetClientes()
        {
            ObservableCollection<Cliente> listaClientes = new ObservableCollection<Cliente>();
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes";
            SqliteDataReader cursorClientes = comando.ExecuteReader();

            if (cursorClientes.HasRows)
            {
                while (cursorClientes.Read())
                {
                    Cliente cliente = new Cliente(cursorClientes.GetInt32(0), (string)cursorClientes["nombre"], (string)cursorClientes["documento"],
                        (string)cursorClientes["foto"], cursorClientes.GetInt32(4), (string)cursorClientes["genero"], (string)cursorClientes["telefono"]);
                    listaClientes.Add(cliente);
                }
            }
            cursorClientes.Close();
            conexion.Close();
            return listaClientes;
        }

        public int AñadirCliente(Cliente cliente)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO clientes (nombre, documento, foto, edad, genero, telefono) " +
                "VALUES (@nombre, @documento, @foto, @edad, @genero, @telefono)";
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters.Add("@foto", SqliteType.Text);
            comando.Parameters.Add("@edad", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@telefono", SqliteType.Text);
            comando.Parameters["@nombre"].Value = cliente.Nombre;
            comando.Parameters["@documento"].Value = cliente.Documento;
            comando.Parameters["@foto"].Value = cliente.Foto;
            comando.Parameters["@edad"].Value = cliente.Edad;
            comando.Parameters["@genero"].Value = cliente.Genero;
            comando.Parameters["@telefono"].Value = cliente.Telefono;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int ModificarCliente(Cliente cliente)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE clientes SET " +
                "nombre = @nombre, documento = @documento, foto = @foto, " +
                "edad = @edad, genero = @genero, telefono = @telefono " +
                "WHERE id_cliente = @idCliente";
            comando.Parameters.Add("@nombre", SqliteType.Text);
            comando.Parameters.Add("@documento", SqliteType.Text);
            comando.Parameters.Add("@foto", SqliteType.Text);
            comando.Parameters.Add("@edad", SqliteType.Integer);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@telefono", SqliteType.Text);
            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters["@nombre"].Value = cliente.Nombre;
            comando.Parameters["@documento"].Value = cliente.Documento;
            comando.Parameters["@foto"].Value = cliente.Foto;
            comando.Parameters["@edad"].Value = cliente.Edad;
            comando.Parameters["@genero"].Value = cliente.Genero;
            comando.Parameters["@telefono"].Value = cliente.Telefono;
            comando.Parameters["@idCliente"].Value = cliente.IdCliente;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int EliminarCliente(Cliente cliente)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM clientes WHERE id_cliente = @idCliente";
            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters["@idCliente"].Value = cliente.IdCliente;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        // Para controlar que no haya dos vehículos con la misma matrícula.
        public Vehiculo GetVehiculoByMatricula(string matricula)
        {
            conexion.Open();
            Vehiculo vehiculo = null;
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos WHERE matricula = @matricula";
            SqliteDataReader cursorVehiculos = comando.ExecuteReader();

            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters["@matricula"].Value = matricula;

            if (cursorVehiculos.HasRows)
            {
                cursorVehiculos.Read();
                vehiculo = new Vehiculo(cursorVehiculos.GetInt32(0), cursorVehiculos.GetInt32(1), (string)cursorVehiculos["matricula"],
                      cursorVehiculos.GetInt32(3), (string)cursorVehiculos["modelo"], (string)cursorVehiculos["tipo"]);
            }
            cursorVehiculos.Close();
            conexion.Close();
            return vehiculo;
        }

        public ObservableCollection<Vehiculo> GetVehiculos()
        {
            ObservableCollection<Vehiculo> listaVehiculos = new ObservableCollection<Vehiculo>();
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM vehiculos";
            SqliteDataReader cursorVehiculos = comando.ExecuteReader();

            if (cursorVehiculos.HasRows)
            {
                while (cursorVehiculos.Read())
                {
                    Vehiculo vehiculo = new Vehiculo(cursorVehiculos.GetInt32(0), cursorVehiculos.GetInt32(1), (string)cursorVehiculos["matricula"],
                        cursorVehiculos.GetInt32(3), (string)cursorVehiculos["modelo"], (string)cursorVehiculos["tipo"]);
                    listaVehiculos.Add(vehiculo);
                }
            }
            cursorVehiculos.Close();
            conexion.Close();
            return listaVehiculos;
        }

        public int AñadirVehiculo(Vehiculo vehiculo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO vehiculos (id_cliente, matricula, id_marca, modelo, tipo) " +
                "VALUES (@idCliente, @matricula, @idMarca, @modelo, @tipo)";
            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters.Add("@idMarca", SqliteType.Integer);
            comando.Parameters.Add("@modelo", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);
            comando.Parameters["@idCliente"].Value = vehiculo.IdCliente;
            comando.Parameters["@matricula"].Value = vehiculo.Matricula;
            comando.Parameters["@idMarca"].Value = vehiculo.IdMarca;
            comando.Parameters["@modelo"].Value = vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = vehiculo.Tipo;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int ModificarVehiculo(Vehiculo vehiculo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE vehiculos SET " +
                "id_cliente = @idCliente, matricula = @matricula, id_marca = @idMarca, " +
                "modelo = @modelo, tipo = @tipo " +
                "WHERE id_vehiculo = @idVehiculo";
            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters.Add("@idMarca", SqliteType.Integer);
            comando.Parameters.Add("@modelo", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);
            comando.Parameters.Add("@idVehiculo", SqliteType.Integer);
            comando.Parameters["@idCliente"].Value = vehiculo.IdCliente;
            comando.Parameters["@matricula"].Value = vehiculo.Matricula;
            comando.Parameters["@idMarca"].Value = vehiculo.IdMarca;
            comando.Parameters["@modelo"].Value = vehiculo.Modelo;
            comando.Parameters["@tipo"].Value = vehiculo.Tipo;
            comando.Parameters["@idVehiculo"].Value = vehiculo.IdVehiculo;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int EliminarVehiculo(Vehiculo vehiculo)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM vehiculos WHERE id_vehiculo = @idVehiculo";
            comando.Parameters.Add("@idVehiculo", SqliteType.Integer);
            comando.Parameters["@idVehiculo"].Value = vehiculo.IdVehiculo;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public ObservableCollection<Estacionamiento> GetEstacionamientos(bool mostrarNoTerminados)
        {
            ObservableCollection<Estacionamiento> listaEstacionamientos = new ObservableCollection<Estacionamiento>();
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos" + (mostrarNoTerminados ? " WHERE salida IS NULL" : "");
            SqliteDataReader cursorEstacionamientos = comando.ExecuteReader();

            if (cursorEstacionamientos.HasRows)
            {
                while (cursorEstacionamientos.Read())
                {
                    Estacionamiento estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), (string)cursorEstacionamientos["matricula"],
                        (string)cursorEstacionamientos["entrada"], (string)cursorEstacionamientos["salida"], cursorEstacionamientos.GetFloat(5), (string)cursorEstacionamientos["tipo"]);
                    listaEstacionamientos.Add(estacionamiento);
                }
            }
            cursorEstacionamientos.Close();
            conexion.Close();
            return listaEstacionamientos;
        }

        public int FinalizarEstacionamiento(Estacionamiento estacionamiento)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE estacionamientos SET " +
                "salida = @salida, importe = @importe " +
                "WHERE id_estacionamiento = @idEstacionamiento";
            comando.Parameters.Add("@importe", SqliteType.Real);
            comando.Parameters.Add("@idEstacionamiento", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters["@importe"].Value = estacionamiento.Importe;
            comando.Parameters["@idEstacionamiento"].Value = estacionamiento.IdEstacionamiento;
            comando.Parameters["@salida"].Value = estacionamiento.Salida;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public ObservableCollection<string> GetMarcas()
        {
            ObservableCollection<string> listaMarcas = new ObservableCollection<string>();
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM marcas";
            SqliteDataReader cursorMarcas = comando.ExecuteReader();

            if (cursorMarcas.HasRows)
            {
                while (cursorMarcas.Read())
                {
                    listaMarcas.Add((string)cursorMarcas["marca"]);
                }
            }
            cursorMarcas.Close();
            conexion.Close();
            return listaMarcas;
        }

        public int AñadirMarca(string marca)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO marcas (marca) " +
                "VALUES (@marca)";
            comando.Parameters.Add("@marca", SqliteType.Text);
            comando.Parameters["@marca"].Value = marca;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        // MÉTODOS PARA AP1
        public int IniciarEstacionamiento(Estacionamiento estacionamiento)
        {
            conexion.Open();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO estacionamientos (id_vehiculo, matricula, entrada, tipo) " +
                "VALUES (@idVehiculo, @matricula, @entrada, @tipo)";
            comando.Parameters.Add("@idVehiculo", SqliteType.Integer);
            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters.Add("@entrada", SqliteType.Text);
            comando.Parameters.Add("@tipo", SqliteType.Text);
            comando.Parameters["@idVehiculo"].Value = estacionamiento.IdVehiculo;
            comando.Parameters["@matricula"].Value = estacionamiento.Matricula;
            comando.Parameters["@entrada"].Value = estacionamiento.Entrada;
            comando.Parameters["@tipo"].Value = estacionamiento.Tipo;
            int filasAfectadas = comando.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        // Para controlar que un mismo vehículo no tenga dos estacionamientos activos al mismo tiempo.
        public Estacionamiento GetEstacionamientoByMatricula(string matricula)
        {
            conexion.Open();
            Estacionamiento estacionamiento = null;
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula = @matricula";
            SqliteDataReader cursorEstacionamientos = comando.ExecuteReader();

            comando.Parameters.Add("@matricula", SqliteType.Text);
            comando.Parameters["@matricula"].Value = matricula;

            if (cursorEstacionamientos.HasRows)
            {
                cursorEstacionamientos.Read();
                estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), (string)cursorEstacionamientos["matricula"],
                        (string)cursorEstacionamientos["entrada"], (string)cursorEstacionamientos["salida"], cursorEstacionamientos.GetFloat(5), (string)cursorEstacionamientos["tipo"]);
            }
            cursorEstacionamientos.Close();
            conexion.Close();
            return estacionamiento;
        }

    }
}
