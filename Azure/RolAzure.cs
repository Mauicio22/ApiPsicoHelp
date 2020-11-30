using ApiPsicoHelp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Azure
{
    public class RolAzure
    {
        static string connectionString = @"Server= localhost; Database=PsicoHelp; Integrated Security=True;";
        private static List<Rol> roles;

        public static List<Rol> obtenerRoles()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlRoles(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarRoles(dataTable);
            }
        }
        private static DataTable LLenadoTabla(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public static Rol ObtenerRol(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Rol where idRol = '{id}'";

                var comando = AbrirConexionSqlRol(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionRol(dataTable);

            }
        }

        private static List<Rol> ListarRoles(DataTable dataTable)
        {
            roles = new List<Rol>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Rol rol = new Rol();
                rol.idRol = int.Parse(dataTable.Rows[i]["idRol"].ToString());
                rol.nombreRol = dataTable.Rows[i]["nombreRol"].ToString();
                rol.idCargo = int.Parse(dataTable.Rows[i]["idCargo"].ToString());
                rol.idRolUsuario = int.Parse(dataTable.Rows[i]["rol_idUsuario"].ToString());
                roles.Add(rol);
            }

            return roles;
        }

        private static SqlCommand AbrirConexionSqlRol(SqlConnection sqlConnection, string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlRoles(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "select * from Rol";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static Rol CreacionRol(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Rol rol = new Rol();
                rol.idRol = int.Parse(dataTable.Rows[0]["idRol"].ToString());
                rol.nombreRol = dataTable.Rows[0]["nombreRol"].ToString();
                rol.idCargo = int.Parse(dataTable.Rows[0]["idCargo"].ToString());
                rol.idRolUsuario = int.Parse(dataTable.Rows[0]["rol_idUsuario"].ToString());
                return rol;
            }
            else
            {
                return null;
            }
        }
        public static int AgregarRol(Rol rol)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Insert into Rol (idRol, nombreRol, idCargo, rol_idUsuario) values (@idRol, @nombreRol, @idCargo, @idRolUsuario)";

                sqlCommand.Parameters.AddWithValue("@idRol", rol.idRol);
                sqlCommand.Parameters.AddWithValue("@nombreRol", rol.nombreRol);
                sqlCommand.Parameters.AddWithValue("@idCargo", rol.idCargo);
                sqlCommand.Parameters.AddWithValue("@idRolUsuario", rol.idRolUsuario);

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
        public static int EliminarRolPorId(int id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Delete from Rol where idRol = @id";

                sqlCommand.Parameters.AddWithValue("@id", id);

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
        public static int ActualizarRol(Rol rol)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Rol SET nombreRol = @nombreRol, rol__idUsuario = @idUsuario where idRol = @idRol";

                sqlCommand.Parameters.AddWithValue("@nombreRol", rol.nombreRol);
                sqlCommand.Parameters.AddWithValue("@idUsuario", rol.idRolUsuario);
                sqlCommand.Parameters.AddWithValue("@idRol", rol.idRol);

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
    }
}
