using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class VentaDal
    {
        private readonly string _connectionString;

        public VentaDal(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertarVenta(Venta venta)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertVenta", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cantidad", venta.Cantidad);
                cmd.Parameters.AddWithValue("@TipoDePago", venta.TipoDePago);
                cmd.Parameters.AddWithValue("@ClienteID", venta.ClienteID);
                cmd.Parameters.AddWithValue("@Precio", venta.Precio);
                cmd.Parameters.AddWithValue("@ProductoID", venta.ProductoID);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}
