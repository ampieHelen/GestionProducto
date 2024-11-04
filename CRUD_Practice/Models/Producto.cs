namespace CRUD_Practice.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }

        public int MarcaID { get; set; }  // Clave foránea
        public Marca Marca { get; set; }

        public int CategoriaID { get; set; }  // Clave foránea
        public Categoria Categoria { get; set; }
    }
}
