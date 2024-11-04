using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CRUD_Practice.DTO;
using CRUD_Practice.Models;
using Microsoft.Extensions.Configuration;

namespace CRUD_Practice.Data
{
    public class ProductoDAL
    {
        private readonly string _connectionString;

        public ProductoDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetProductos", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var producto = new Producto
                        {
                            ProductoID = (int)reader["ProductoID"],
                            Nombre = reader["ProductoNombre"].ToString(),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = (decimal)reader["Precio"],
                            Marca = new Marca
                            {
                                Nombre = reader["MarcaNombre"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                Nombre = reader["CategoriaNombre"].ToString()
                            }
                        };
                        productos.Add(producto);
                    }
                }
            }
            return productos;
        }


        public void InsertarProducto(ProductoDTO producto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spInsertProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@MarcaID", producto.MarcaID);
                cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ActualizarProducto(ProductoDTO producto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@MarcaID", producto.MarcaID);
                cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarProducto(int productoID)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProducto", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductoID", productoID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
