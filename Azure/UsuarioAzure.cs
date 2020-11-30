using ApiPsicoHelp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Azure
{
    public class UsuarioAzure
    {
        static string connectionString = @"Server= localhost; Database=PsicoHelp; Integrated Security=True;";

        private static List<Usuario> usuarios;

        public static List<Usuario> obtenerUsuarios()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlUsuarios(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarUsuarios(dataTable);
            }
        }

        private static List<Usuario> ListarUsuarios(DataTable dataTable)
        {
            usuarios = new List<Usuario>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Usuario usuario = new Usuario();
                usuario.idUsuario = int.Parse(dataTable.Rows[i]["idUsuario"].ToString());
                usuario.rut = dataTable.Rows[i]["rutUsuario"].ToString();
                usuario.app = dataTable.Rows[i]["apellidoPaterno"].ToString();
                usuario.apm = dataTable.Rows[i]["apellidoMaterno"].ToString();
                usuario.fechaNacimiento = DateTime.Parse(dataTable.Rows[i]["fechaNacimiento"].ToString());
                usuario.genero = dataTable.Rows[i]["genero"].ToString();
                usuarios.Add(usuario);
            }

            return usuarios;
        }
        public static Usuario ObtenerUsuario(string rut)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Usuario where rutUsuario = '{rut}'";

                var comando = AbrirConexionSqlUsuario(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionUsuario(dataTable);

            }
        }
        public static Usuario ObtenerUsuario(int idUsuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Usuario where idUsuario = '{idUsuario}'";

                var comando = AbrirConexionSqlUsuario(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionUsuario(dataTable);

            }
        }
        public static int AgregarUsuario(Usuario usuario)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Insert into Usuario (idUsuario, rutUsuario, apellidoPaterno, apellidoMaterno, fechaNacimiento, genero) values (@idUsuario, @rutUsuario, @apellidoPaterno, @apellidoMaterno, @fechaNacimiento, @genero)";

                sqlCommand.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                sqlCommand.Parameters.AddWithValue("@rutUsuario", usuario.rut);
                sqlCommand.Parameters.AddWithValue("@apellidoPaterno", usuario.app);
                sqlCommand.Parameters.AddWithValue("@apellidoMaterno", usuario.apm);
                sqlCommand.Parameters.AddWithValue("@fechaNacimiento", usuario.fechaNacimiento);
                sqlCommand.Parameters.AddWithValue("@genero", usuario.genero);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }
        public static int EliminarUsuarioPorRut(string rut)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Delete from Usuario where rutUsuario = @rut";

                sqlCommand.Parameters.AddWithValue("@rut", rut);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }

        public static int ActualizarUsuario(Usuario usuario)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Usuario SET apellidoPaterno = @app, apellidoMaterno = @apm where rutUsuario = @rutUsuario";

                sqlCommand.Parameters.AddWithValue("@app", usuario.app);
                sqlCommand.Parameters.AddWithValue("@apm", usuario.apm);
                sqlCommand.Parameters.AddWithValue("@rutUsuario", usuario.rut);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }
        private static DataTable LLenadoTabla(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static SqlCommand AbrirConexionSqlUsuario(SqlConnection sqlConnection, string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            return sqlCommand;
        }


        private static SqlCommand AbrirConexionSqlUsuarios(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "select * from Usuario";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static Usuario CreacionUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Usuario usuario = new Usuario();
                usuario.idUsuario = int.Parse(dataTable.Rows[0]["idUsuario"].ToString());
                usuario.rut = dataTable.Rows[0]["rutUsuario"].ToString();
                usuario.app = dataTable.Rows[0]["apellidoPaterno"].ToString();
                usuario.apm = dataTable.Rows[0]["apellidoMaterno"].ToString();
                usuario.fechaNacimiento = DateTime.Parse(dataTable.Rows[0]["fechaNacimiento"].ToString());
                usuario.genero = dataTable.Rows[0]["genero"].ToString();
                return usuario;
            }
            else
            {
                return null;
            }
        }
    }
    }


