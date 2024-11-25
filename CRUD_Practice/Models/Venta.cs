namespace CRUD_Practice.Models
{
    public class Venta
    {
        public int Cantidad { get; set; }
        public string TipoDePago { get; set; }
        public int ClienteID { get; set; }
        public decimal Precio { get; set; }
        public int ProductoID { get; set; }
    }
}
