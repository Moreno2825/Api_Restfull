using ApiQuick2Go.Entidades;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata.Ecma335;

namespace ApiQuick2Go.Entidades
{
    
    public class Pedido 
    {


        public int Id { get; set; }
        public int IdComprador { get; set; }
        public int IdProducto { get; set; }
        public int CantidadProducto { get; set; }
        public double SubTotal { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario{ get; set; }
        public List<Comprador> Compradores { get; set; }
        public List<Producto> Productos { get; set; }
    }
}
