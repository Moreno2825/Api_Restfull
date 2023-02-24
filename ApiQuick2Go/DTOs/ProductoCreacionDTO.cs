using ApiQuick2Go.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiQuick2Go.DTOs
{
    public class ProductoCreacionDTO
    {

        [Required]
        public string NombreProducto { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        [PrimeraLetraMayus]
        public string Marca { get; set; }

        [Required]
        public double Precio { get; set; }
    }
}
