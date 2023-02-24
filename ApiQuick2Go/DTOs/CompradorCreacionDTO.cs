using ApiQuick2Go.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiQuick2Go.DTOs
{
    public class CompradorCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido...")]
        [PrimeraLetraMayus]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido...")]
        [PrimeraLetraMayus]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido...")]
        [PrimeraLetraMayus]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido...")]
        public string NumeroTelefono { get; set; }
    }
}
