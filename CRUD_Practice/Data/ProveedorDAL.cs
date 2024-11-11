using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class ProveedorDAL
    {
        private readonly string _connectionString;

        public ProveedorDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Proveedor> ObtenerProveedores()
        {
            var proveedores = new List<Proveedor>();
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetProveedores", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        proveedores.Add(new Proveedor
                        {
                            ProveedorID = (int)reader["ProveedorID"],
                            Nombre = reader["Nombre"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Telefono = reader["Telefono"].ToString()
                        });
                    }
                }
            }
            return proveedores;
        }

        public void InsertarProveedor(Proveedor proveedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertProveedor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", proveedor.Apellido);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarProveedor(Proveedor proveedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateProveedor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProveedorID", proveedor.ProveedorID);
                cmd.Parameters.AddWithValue("@Nombre", proveedor.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", proveedor.Apellido);
                cmd.Parameters.AddWithValue("@Direccion", proveedor.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", proveedor.Telefono);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProveedor(int proveedorID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProveedor", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@proveedorID", proveedorID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
