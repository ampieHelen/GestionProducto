using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class CompraDAL
    {
        private readonly string _connectionString;

        public CompraDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertarCompra(Compra compra)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertCompra", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cantidad", compra.Cantidad);
                cmd.Parameters.AddWithValue("@TipoDePago", compra.TipoDePago);
                cmd.Parameters.AddWithValue("@ProveedorID", compra.ProveedorID);
                cmd.Parameters.AddWithValue("@Costo", compra.Costo);
                cmd.Parameters.AddWithValue("@ProductoID", compra.ProductoID);
                conn.Open();
                cmd.ExecuteNonQuery();
               
            }
        }
    }
}
