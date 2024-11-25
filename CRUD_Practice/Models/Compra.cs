namespace CRUD_Practice.Models
{
    public class Compra
    {
        public int Cantidad { get; set; }
        public string TipoDePago { get; set; }
        public int ProveedorID { get; set; }
        public decimal Costo { get; set; }
        public int ProductoID { get; set; }
    }
}
