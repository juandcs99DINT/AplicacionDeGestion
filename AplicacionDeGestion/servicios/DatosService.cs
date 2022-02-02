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
        private readonly DialogosService dialogosService = new DialogosService();
        private readonly SqliteConnection conexion = new SqliteConnection("Data Source=" + Properties.Settings.Default.rutaConexionBd + "parking.db");

        // Para controlar que no haya dos clientes con el mismo documento.
        public Cliente GetClienteByDocument(string documento)
        {
            Cliente cliente = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM clientes WHERE documento = @documento";
                comando.Parameters.Add("@documento", SqliteType.Text);
                comando.Parameters["@documento"].Value = documento;
                SqliteDataReader cursorClientes = comando.ExecuteReader();

                if (cursorClientes.HasRows)
                {
                    cursorClientes.Read();
                    cliente = new Cliente(cursorClientes.GetInt32(0), cursorClientes["nombre"] != DBNull.Value ? (string)cursorClientes["nombre"] : "", cursorClientes["documento"] != DBNull.Value ? (string)cursorClientes["documento"] : "",
                               cursorClientes["foto"] != DBNull.Value ? (string)cursorClientes["foto"] : "", cursorClientes.GetInt32(4),
                               cursorClientes["genero"] != DBNull.Value ? (string)cursorClientes["genero"] : "", cursorClientes["telefono"] != DBNull.Value ? (string)cursorClientes["telefono"] : "");
                }
                cursorClientes.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return cliente;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM clientes WHERE id_cliente = @idCliente";
                comando.Parameters.Add("@idCliente", SqliteType.Integer);
                comando.Parameters["@idCliente"].Value = id;

                SqliteDataReader cursorClientes = comando.ExecuteReader();

                if (cursorClientes.HasRows)
                {
                    cursorClientes.Read();
                    cliente = new Cliente(cursorClientes.GetInt32(0), cursorClientes["nombre"] != DBNull.Value ? (string)cursorClientes["nombre"] : "", cursorClientes["documento"] != DBNull.Value ? (string)cursorClientes["documento"] : "",
                               cursorClientes["foto"] != DBNull.Value ? (string)cursorClientes["foto"] : "", cursorClientes.GetInt32(4),
                               cursorClientes["genero"] != DBNull.Value ? (string)cursorClientes["genero"] : "", cursorClientes["telefono"] != DBNull.Value ? (string)cursorClientes["telefono"] : "");
                }
                cursorClientes.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return cliente;
        }

        public ObservableCollection<Cliente> GetClientes()
        {
            ObservableCollection<Cliente> listaClientes = new ObservableCollection<Cliente>();
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM clientes";
                SqliteDataReader cursorClientes = comando.ExecuteReader();

                if (cursorClientes.HasRows)
                {
                    while (cursorClientes.Read())
                    {
                        Cliente cliente = new Cliente(cursorClientes.GetInt32(0), cursorClientes["nombre"] != DBNull.Value ? (string)cursorClientes["nombre"] : "", cursorClientes["documento"] != DBNull.Value ? (string)cursorClientes["documento"] : "",
                            cursorClientes["foto"] != DBNull.Value ? (string)cursorClientes["foto"] : "", cursorClientes.GetInt32(4),
                            cursorClientes["genero"] != DBNull.Value ? (string)cursorClientes["genero"] : "", cursorClientes["telefono"] != DBNull.Value ? (string)cursorClientes["telefono"] : "");
                        listaClientes.Add(cliente);
                    }
                }
                cursorClientes.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return listaClientes;
        }

        public int AñadirCliente(Cliente cliente)
        {
            int filasAfectadas = 0;
            try
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
                comando.Parameters["@nombre"].Value = cliente.Nombre ?? "";
                comando.Parameters["@documento"].Value = cliente.Documento;
                comando.Parameters["@foto"].Value = cliente.Foto ?? "";
                comando.Parameters["@edad"].Value = cliente.Edad ?? 0;
                comando.Parameters["@genero"].Value = cliente.Genero ?? "";
                comando.Parameters["@telefono"].Value = cliente.Telefono ?? "";
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public int ModificarCliente(Cliente cliente)
        {
            int filasAfectadas = 0;
            try
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
                comando.Parameters["@nombre"].Value = cliente.Nombre ?? "";
                comando.Parameters["@documento"].Value = cliente.Documento;
                comando.Parameters["@foto"].Value = cliente.Foto ?? "";
                comando.Parameters["@edad"].Value = cliente.Edad ?? 0;
                comando.Parameters["@genero"].Value = cliente.Genero ?? "";
                comando.Parameters["@telefono"].Value = cliente.Telefono ?? "";
                comando.Parameters["@idCliente"].Value = cliente.IdCliente;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public int EliminarCliente(Cliente cliente)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "DELETE FROM clientes WHERE id_cliente = @idCliente";
                comando.Parameters.Add("@idCliente", SqliteType.Integer);
                comando.Parameters["@idCliente"].Value = cliente.IdCliente;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        // Para controlar que no haya dos vehículos con la misma matrícula.
        public Vehiculo GetVehiculoByMatricula(string matricula)
        {
            Vehiculo vehiculo = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM vehiculos WHERE matricula = @matricula";
                SqliteDataReader cursorVehiculos = comando.ExecuteReader();

                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;

                if (cursorVehiculos.HasRows)
                {

                    cursorVehiculos.Read();
                    vehiculo = new Vehiculo(cursorVehiculos.GetInt32(0), cursorVehiculos.GetInt32(1), cursorVehiculos["matricula"] != DBNull.Value ? (string)cursorVehiculos["matricula"] : "",
                          cursorVehiculos.GetInt32(3), cursorVehiculos["modelo"] != DBNull.Value ? (string)cursorVehiculos["modelo"] : "", cursorVehiculos["tipo"] != DBNull.Value ? (string)cursorVehiculos["tipo"] : "");
                }
                cursorVehiculos.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return vehiculo;
        }

        public ObservableCollection<Vehiculo> GetVehiculos()
        {
            ObservableCollection<Vehiculo> listaVehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM vehiculos";
                SqliteDataReader cursorVehiculos = comando.ExecuteReader();

                if (cursorVehiculos.HasRows)
                {
                    while (cursorVehiculos.Read())
                    {
                        Vehiculo vehiculo = new Vehiculo(cursorVehiculos.GetInt32(0), cursorVehiculos.GetInt32(1), cursorVehiculos["matricula"] != DBNull.Value ? (string)cursorVehiculos["matricula"] : "",
                          cursorVehiculos.GetInt32(3), cursorVehiculos["modelo"] != DBNull.Value ? (string)cursorVehiculos["modelo"] : "", cursorVehiculos["tipo"] != DBNull.Value ? (string)cursorVehiculos["tipo"] : "");
                        listaVehiculos.Add(vehiculo);
                    }
                }
                cursorVehiculos.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return listaVehiculos;
        }

        public int AñadirVehiculo(Vehiculo vehiculo)
        {
            int filasAfectadas = 0;
            try
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
                comando.Parameters["@idMarca"].Value = vehiculo.IdMarca ?? 0;
                comando.Parameters["@modelo"].Value = vehiculo.Modelo ?? "";
                comando.Parameters["@tipo"].Value = vehiculo.Tipo;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public int ModificarVehiculo(Vehiculo vehiculo)
        {
            int filasAfectadas = 0;
            try
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
                comando.Parameters["@idMarca"].Value = vehiculo.IdMarca ?? 0;
                comando.Parameters["@modelo"].Value = vehiculo.Modelo ?? "";
                comando.Parameters["@tipo"].Value = vehiculo.Tipo;
                comando.Parameters["@idVehiculo"].Value = vehiculo.IdVehiculo;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public int EliminarVehiculo(Vehiculo vehiculo)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "DELETE FROM vehiculos WHERE id_vehiculo = @idVehiculo";
                comando.Parameters.Add("@idVehiculo", SqliteType.Integer);
                comando.Parameters["@idVehiculo"].Value = vehiculo.IdVehiculo;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public ObservableCollection<Estacionamiento> GetEstacionamientos(bool mostrarNoTerminados)
        {
            ObservableCollection<Estacionamiento> listaEstacionamientos = new ObservableCollection<Estacionamiento>();
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM estacionamientos" + (mostrarNoTerminados ? " WHERE salida IS NULL" : "");
                SqliteDataReader cursorEstacionamientos = comando.ExecuteReader();

                if (cursorEstacionamientos.HasRows)
                {
                    while (cursorEstacionamientos.Read())
                    {
                        Estacionamiento estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                            cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "", cursorEstacionamientos["salida"] != DBNull.Value ? (string)cursorEstacionamientos["salida"] : "",
                            cursorEstacionamientos.GetFloat(5), cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                        listaEstacionamientos.Add(estacionamiento);
                    }
                }
                cursorEstacionamientos.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }

            return listaEstacionamientos;
        }

        public int FinalizarEstacionamiento(Estacionamiento estacionamiento)
        {
            int filasAfectadas = 0;
            try
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
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public ObservableCollection<string> GetMarcas()
        {
            ObservableCollection<string> listaMarcas = new ObservableCollection<string>();
            try
            {
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
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return listaMarcas;
        }

        public int AñadirMarca(string marca)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO marcas (marca) " +
                    "VALUES (@marca)";
                comando.Parameters.Add("@marca", SqliteType.Text);
                comando.Parameters["@marca"].Value = marca;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        public int IniciarEstacionamiento(Estacionamiento estacionamiento)
        {
            int filasAfectadas = 0;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "INSERT INTO estacionamientos (id_vehiculo, matricula, entrada, tipo) " +
                    "VALUES (@idVehiculo, @matricula, @entrada, @tipo)";
                comando.Parameters.Add("@idVehiculo", SqliteType.Integer);
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters.Add("@entrada", SqliteType.Text);
                comando.Parameters.Add("@tipo", SqliteType.Text);
                comando.Parameters["@idVehiculo"].Value = estacionamiento.IdVehiculo ?? 0;
                comando.Parameters["@matricula"].Value = estacionamiento.Matricula;
                comando.Parameters["@entrada"].Value = estacionamiento.Entrada;
                comando.Parameters["@tipo"].Value = estacionamiento.Tipo;
                filasAfectadas = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return filasAfectadas;
        }

        // Para controlar que un mismo vehículo no tenga dos estacionamientos activos al mismo tiempo.
        public Estacionamiento GetEstacionamientoByMatricula(string matricula)
        {
            Estacionamiento estacionamiento = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula = @matricula";
                SqliteDataReader cursorEstacionamientos = comando.ExecuteReader();

                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;

                if (cursorEstacionamientos.HasRows)
                {
                    cursorEstacionamientos.Read();
                    estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                                cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "", cursorEstacionamientos["salida"] != DBNull.Value ? (string)cursorEstacionamientos["salida"] : "",
                                cursorEstacionamientos.GetFloat(5), cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                    cursorEstacionamientos.Close();
                    conexion.Close();
                }
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return estacionamiento;
        }
    }
}
