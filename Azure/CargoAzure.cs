using ApiPsicoHelp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPsicoHelp.Azure
{
    public class CargoAzure
    {
        static string connectionString = @"Server= localhost; Database=PsicoHelp; Integrated Security=True;";
        private static List<Cargo> cargos;

        public static List<Cargo> obtenerCargos()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlCargos(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarCargos(dataTable);
            }
        }
        private static SqlCommand AbrirConexionSqlCargos(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "select * from Cargo";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static DataTable LLenadoTabla(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<Cargo> ListarCargos(DataTable dataTable)
        {
            cargos = new List<Cargo>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Cargo cargo = new Cargo();
                cargo.idCargo = int.Parse(dataTable.Rows[i]["idCargo"].ToString());
                cargo.nombreCargo = dataTable.Rows[i]["nombreCargo"].ToString();
                cargo.idUsuario = int.Parse(dataTable.Rows[i]["idUsuario"].ToString());
                cargos.Add(cargo);
            }

            return cargos;
        }
        public static Cargo obtenerCargoPorId(int idCargo)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"select * from Cargo where idCargo = '{idCargo}'";

                var comando = AbrirConexionSqlCargo(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionCargo(dataTable);

            }
        }
        
        private static SqlCommand AbrirConexionSqlCargo(SqlConnection sqlConnection, string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            return sqlCommand;
        }
        private static Cargo CreacionCargo(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Cargo cargo = new Cargo();
                cargo.idCargo = int.Parse(dataTable.Rows[0]["idCargo"].ToString());
                cargo.nombreCargo = dataTable.Rows[0]["nombreCargo"].ToString();
                cargo.idUsuario = int.Parse(dataTable.Rows[0]["idUsuario"].ToString());
                return cargo;
            }
            else
            {
                return null;
            }
        }
        public static int AgregarCargo(Cargo cargo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Insert into Cargo (idCargo, nombreCargo, idUsuario) values (@idCargo, @nombreCargo, @idUsuario)";

                sqlCommand.Parameters.AddWithValue("@idCargo", cargo.idCargo);
                sqlCommand.Parameters.AddWithValue("@nombreCargo", cargo.nombreCargo);
                sqlCommand.Parameters.AddWithValue("@idUsuario", cargo.idUsuario);

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
        public static int EliminarCargo(int idCargo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Delete from Cargo where idCargo = @idCargo";

                sqlCommand.Parameters.AddWithValue("@idCargo", idCargo);

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
        public static int ActualizarCargo(Cargo cargo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "Update Cargo SET nombreCargo = @nombreCargo WHERE idCargo = @idCargo";

                sqlCommand.Parameters.AddWithValue("@nombreCargo", cargo.nombreCargo);
                sqlCommand.Parameters.AddWithValue("@idCargo", cargo.idCargo);


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
