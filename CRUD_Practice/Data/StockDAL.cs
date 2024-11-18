using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
   public class StockDAL
    {
        private readonly string _connectionString;

        public StockDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Stock> ObtenerStock()
        {
            var stock = new List<Stock>();
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetStock", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stock.Add(new Stock
                        {
                            ProductoID = (int)reader["ProductoID"],
                            Producto = reader["Producto"].ToString(),
                            Cantidad = (int)reader["Cantidad"],
                            Precio = (decimal)reader["Precio"],
                            Categoria = reader["Categoria"].ToString(),
                            Marca = reader["Marca"].ToString()
                        });
                    }
                }
            }
            return stock;
        }
    }
}
