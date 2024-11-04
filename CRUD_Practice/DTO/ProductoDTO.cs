namespace CRUD_Practice.DTO
{
    public class ProductoDTO
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public int MarcaID { get; set; }
        public int CategoriaID { get; set; }
    }
}
