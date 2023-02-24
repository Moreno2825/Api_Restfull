using System.ComponentModel.DataAnnotations;

namespace ApiQuick2Go.Validaciones
{
    public class PrimeraLetraMayus : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }
        
            var firtsletter = value.ToString()[0].ToString();
        
            if (firtsletter != firtsletter.ToUpper())
            {
                return new ValidationResult("La primera letra debe de ser mayúscula...");
            }
        
            return ValidationResult.Success;
        }
        
    }
}
