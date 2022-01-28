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


        public Cliente GetCliente(int id)
        {
            conexion.Open();
            Cliente cliente = new Cliente();
            SqliteCommand comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM clientes WHERE id_cliente = @idCliente";
            SqliteDataReader cursorClientes = comando.ExecuteReader();

            comando.Parameters.Add("@idCliente", SqliteType.Integer);
            comando.Parameters["@idCliente"].Value = id;

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


    }
}
