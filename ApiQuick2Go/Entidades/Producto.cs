using ApiQuick2Go.DTOs;
using ApiQuick2Go.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiQuick2Go.Entidades
{
    public class Producto
    {

        
        public int Id { get; set; }
        
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
