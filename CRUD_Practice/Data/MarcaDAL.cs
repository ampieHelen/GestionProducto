using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class MarcaDAL
    {
        private readonly string _connectionString;

        public MarcaDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Marca> ObtenerMarcas()
        {
            var marcas = new List<Marca>();
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetMarcas", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marcas.Add(new Marca
                        {
                            MarcaID = (int)reader["MarcaID"],
                            Nombre = reader["Nombre"].ToString(),

                        });
                    }
                }
            }
            return marcas;
        }

        public void InsertarMarca(Marca marca)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertMarca", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", marca.Nombre);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarMarca(Marca marca)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateMarca", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MarcaID", marca.MarcaID);
                cmd.Parameters.AddWithValue("@Nombre", marca.Nombre);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarMarca(int marcaID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteMarca", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MarcaID", marcaID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
