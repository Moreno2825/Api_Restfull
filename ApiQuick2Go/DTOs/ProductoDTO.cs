using ApiQuick2Go.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ApiQuick2Go.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }

        
        public string NombreProducto { get; set; }

        
        public string Descripcion { get; set; }

        
        public string Categoria { get; set; }

        public string Marca { get; set; }

        public double Precio { get; set; }
    }
}
