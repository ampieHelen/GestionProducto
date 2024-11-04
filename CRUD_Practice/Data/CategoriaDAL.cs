using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class CategoriaDAL
    {
        private readonly string _connectionString;

        public CategoriaDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Categoria> ObtenerCategorias()
        {
            var categorias = new List<Categoria>();
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetCategorias", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categorias.Add(new Categoria
                        {
                            CategoriaID = (int)reader["CategoriaID"],
                            Nombre = reader["Nombre"].ToString(),

                        });
                    }
                }
            }
            return categorias;
        }

        public void InsertarCategoria(Categoria categoria)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertCategoria", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarCategoria(Categoria categoria)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateCategoria", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoriaID", categoria.CategoriaID);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarCategoria(int categoriaID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteCategoria", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
