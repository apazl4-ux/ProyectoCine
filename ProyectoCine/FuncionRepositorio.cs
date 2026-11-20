using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoCine
{
    public class FuncionRepositorio
    {
        private const string CadenaConexion =
                @"Server=(localdb)\MSSQLLocalDB;" +
                @"Database=CineDB;" +
                @"Trusted_Connection=True;" +
                @"TrustServerCertificate=True;";
        public void Insertar(Funcion p)
        {
            string sql = "INSERT INTO dbo.Funciones (Nombre, Sala, Precio) " +
                         "VALUES (@nombre, @sala, @precio)";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.Parameters.AddWithValue("@sala", p.Sala);
                comando.Parameters.AddWithValue("@precio", p.Precio);


                conexion.Open();
                int filas = comando.ExecuteNonQuery();
                Console.WriteLine(filas == 1 ? "Funcion insertada" : "No se insertó.");
            }
        }
        public List<Funcion> ListarProductos()
        {
            List<Funcion> Lista = new List<Funcion>();
            string sql = "SELECT Id, Nombre, Sala, Precio FROM dbo.Funciones";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Lista.Add(new Funcion
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Sala = lector.GetString(2),
                            Precio = lector.GetDecimal(3),
                        });
                    }
                }
            }
            return Lista;
        }
        public Funcion? BuscarPorId(int id)
        {
            string Sql = "SELECT Id, Nombre, Sala, Precio " +
                "FROM dbo.Funciones WHERE Id = @id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(Sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        return new Funcion
                        {
                            Id = lector.GetInt32(0),
                            Nombre = lector.GetString(1),
                            Sala = lector.GetString(2),
                            Precio = lector.GetDecimal(3),
                        };
                    }
                }
            }
            return null;
        }
        public bool Actualizar(Funcion p)
        {
            string Sql = "UPDATE dbo.Funciones " +
                "SET Nombre = @nombre, Sala = @sala, Precio = @precio " +
                "Where Id = @id";

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(Sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", p.Id);
                comando.Parameters.AddWithValue("@nombre", p.Nombre);
                comando.Parameters.AddWithValue("@sala", p.Sala);
                comando.Parameters.AddWithValue("@precio", p.Precio);

                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }
        public bool Eliminar(int id)
        {
            string Sql = "DELETE FROM dbo.Funciones WHERE Id = @id";
            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            using (SqlCommand comando = new SqlCommand(Sql, conexion))
            {
                comando.Parameters.AddWithValue("@id", id);
                conexion.Open();
                return comando.ExecuteNonQuery() == 1;
            }
        }
    }
}