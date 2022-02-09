﻿using AplicacionDeGestion.modelos;
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

        /// <summary>
        /// Obtiene un cliente según el documento pasado por parámetro.
        /// </summary>
        /// <param name="documento">Documento del cliente</param>
        /// <returns>Clase Cliente</returns>
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

        /// <summary>
        /// Obtiene un cliente según el id pasado por parámetro.
        /// </summary>
        /// <param name="id">Id del cliente</param>
        /// <returns>Clase Cliente</returns>
        public Cliente GetClienteById(int? id)
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

        /// <summary>
        /// Obtiene la lista de clientes completa de la base de datos.
        /// </summary>
        /// <returns>Lista de clientes</returns>
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

        /// <summary>
        /// Añade el cliente pasado por parámetro a la base de datos.
        /// </summary>
        /// <param name="cliente">Cliente a añadir</param>
        /// <returns>Registros insertados</returns>
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

        /// <summary>
        /// Modifica el cliente existente pasado por parámetro en la base de datos.
        /// </summary>
        /// <param name="cliente">Cliente a modificar</param>
        /// <returns>Registros modificados</returns>
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

        /// <summary>
        /// Elimina un cliente pasado por parámetro de la base de datos
        /// </summary>
        /// <param name="cliente">Cliente a eliminar</param>
        /// <returns>Registros eliminados</returns>
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

        /// <summary>
        /// Obtiene un vehículo según su matrícula pasada por parámetro. Se utiliza para controlar
        /// que no haya más de un vehículo con la misma matrícula en la base de datos.
        /// </summary>
        /// <param name="matricula">Matrícula a buscar</param>
        /// <returns>Vehículo con esa matricula o null si no existe</returns>
        public Vehiculo GetVehiculoByMatricula(string matricula)
        {
            Vehiculo vehiculo = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM vehiculos WHERE matricula = @matricula";
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;
                SqliteDataReader cursorVehiculos = comando.ExecuteReader();

                if (cursorVehiculos.HasRows)
                {

                    cursorVehiculos.Read();
                    vehiculo = new Vehiculo(cursorVehiculos.GetInt32(0), cursorVehiculos.GetInt32(1), cursorVehiculos["matricula"] != DBNull.Value ? (string)cursorVehiculos["matricula"] : "",
                          cursorVehiculos.GetInt32(3), cursorVehiculos["modelo"] != DBNull.Value ? (string)cursorVehiculos["modelo"] : "", cursorVehiculos["tipo"] != DBNull.Value ? (string)cursorVehiculos["tipo"] : "");
                }
                cursorVehiculos.Close();
                conexion.Close();
            }
            catch (Exception)
            {
                dialogosService.DialogoError("Error obteniendo el vehículo por matrícula");
            }
            return vehiculo;
        }

        /// <summary>
        /// Obtiene la lista de vehículos pertenecientes a un cliente según su Id.
        /// </summary>
        /// <param name="idCliente">Id del cliente</param>
        /// <returns>Lista de vehículos pertenecientes al cliente</returns>
        public ObservableCollection<Vehiculo> GetVehiculosByIdCliente(int? idCliente)
        {
            ObservableCollection<Vehiculo> listaVehiculos = new ObservableCollection<Vehiculo>();
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM vehiculos WHERE id_cliente = @idCliente";
                comando.Parameters.Add("@idCliente", SqliteType.Integer);
                comando.Parameters["@idCliente"].Value = idCliente;
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
            catch (Exception)
            {
                dialogosService.DialogoError("Error obteniendo el vehículo por el ID del cliente.");
            }
            return listaVehiculos;
        }


        /// <summary>
        /// Obtiene la lista de vehículos completa de la base de datos.
        /// </summary>
        /// <returns>Lista de vehículos</returns>
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

        /// <summary>
        /// Añade un vehículo pasado por parámetro a la base de datos.
        /// </summary>
        /// <param name="vehiculo">Vehículo a añadir</param>
        /// <returns>Registros insertados</returns>
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
            catch (Exception)
            {
                dialogosService.DialogoError("Error añadiendo un vehículo");
            }
            return filasAfectadas;
        }

        /// <summary>
        /// Modifica un vehículo ya existente pasado por parámetro en la base de datos.
        /// </summary>
        /// <param name="vehiculo">Vehículo a modificar</param>
        /// <returns>Registros modificados</returns>
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

        /// <summary>
        /// Elimina un vehículo de la base de datos.
        /// </summary>
        /// <param name="vehiculo">Vehiculo a eliminar</param>
        /// <returns>Registros eliminados</returns>
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

        /// <summary>
        /// Devuelve una lista de tipos de vehículo.
        /// </summary>
        /// <returns>Lista de string</returns>
        public ObservableCollection<string> GetTipoVehiculos()
        {
            return new ObservableCollection<string>
            {
                "Coche", "Moto"
            };
        }

        /// <summary>
        /// Obtiene una lista de vehículos de la base de datos, que mostrará todos o solo los finalizados
        /// según el valor pasado por parámetro.
        /// </summary>
        /// <param name="mostrarNoTerminados">Parámetro para filtrar por terminados o no</param>
        /// <returns>Lista de estacionamientos</returns>
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
                        Estacionamiento estacionamiento;
                        if (!cursorEstacionamientos.IsDBNull(1))
                        {
                            estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                                cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "",
                                cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                        }
                        else
                        {
                            estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                                cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "",
                                cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                        }
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

        /// <summary>
        ///  Actualiza algunos campos en la base de datos para dar el estacionamiento por finalizado.
        /// </summary>
        /// <param name="estacionamiento">Estacionamiento a finalizar</param>
        /// <returns>Registros modificados</returns>
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
                comando.Parameters.Add("@salida", SqliteType.Text);
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

        /// <summary>
        /// Devuelve una lista de marcas guardadas en la base de datos.
        /// </summary>
        /// <returns>Lista de marcas</returns>
        public ObservableCollection<Marca> GetMarcas()
        {
            ObservableCollection<Marca> listaMarcas = new ObservableCollection<Marca>();
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
                        listaMarcas.Add(new Marca(cursorMarcas.GetInt32(0), (string)cursorMarcas["marca"]));
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

        /// <summary>
        /// Devuelve un objeto marca según su id pasado por parámetro
        /// </summary>
        /// <param name="id">Id de la marca</param>
        /// <returns>Marca con ese id o null si no existe</returns>
        public Marca GetMarcaById(int? id)
        {
            Marca marca = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM marcas WHERE id_marca = @idMarca";
                comando.Parameters.Add("@idMarca", SqliteType.Integer);
                comando.Parameters["@idMarca"].Value = id;
                SqliteDataReader cursorMarcas = comando.ExecuteReader();
                if (cursorMarcas.HasRows)
                {
                    cursorMarcas.Read();
                    marca = new Marca(cursorMarcas.GetInt32(0), (string)cursorMarcas["marca"]);
                }
                cursorMarcas.Close();
                conexion.Close();
            }
            catch (Exception e)
            {
                dialogosService.DialogoError(e.Message);
            }
            return marca;
        }

        /// <summary>
        /// Añade una nueva marca a la base de datos.
        /// </summary>
        /// <param name="marca">Nombre de la marca</param>
        /// <returns>Registros insertados</returns>
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

        /// <summary>
        /// Obtiene el estacionamiento según su matrícula pasada por parámetro.
        /// </summary>
        /// <param name="matricula">Matricula del vehiculo estacionado</param>
        /// <returns>Estacionamiento con esa matricula o null si no existe</returns>
        public Estacionamiento GetEstacionamientoByMatricula(string matricula)
        {
            Estacionamiento estacionamiento = null;
            try
            {
                conexion.Open();
                SqliteCommand comando = conexion.CreateCommand();
                comando.CommandText = "SELECT * FROM estacionamientos WHERE matricula = @matricula";
                comando.Parameters.Add("@matricula", SqliteType.Text);
                comando.Parameters["@matricula"].Value = matricula;
                SqliteDataReader cursorEstacionamientos = comando.ExecuteReader();
                if (cursorEstacionamientos.HasRows)
                {
                    cursorEstacionamientos.Read();
                    if (!cursorEstacionamientos.IsDBNull(1))
                    {
                        estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos.GetInt32(1), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                            cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "",
                            cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                    }
                    else
                    {
                        estacionamiento = new Estacionamiento(cursorEstacionamientos.GetInt32(0), cursorEstacionamientos["matricula"] != DBNull.Value ? (string)cursorEstacionamientos["matricula"] : "",
                            cursorEstacionamientos["entrada"] != DBNull.Value ? (string)cursorEstacionamientos["entrada"] : "",
                            cursorEstacionamientos["tipo"] != DBNull.Value ? (string)cursorEstacionamientos["tipo"] : "");
                    }
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
