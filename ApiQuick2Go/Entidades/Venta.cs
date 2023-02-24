using System.Reflection.Metadata.Ecma335;
using ApiQuick2Go.Entidades;



namespace ApiQuick2Go.Entidades
{
    public class Venta
    {
        private double iva = .16;
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public double TotalPago { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}
