using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using ProductsValidation.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProductsValidation.Validations
{
    public class StartsWithNameAttribute : ValidationAttribute
    {
        private string _name;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var product = (Product)validationContext.ObjectInstance;
            _name = product.Name;
            string pattern = $"^{_name}.+";
            var r = new Regex(pattern);
            if (r.IsMatch((string)value))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());
        }

        public string GetErrorMessage()
        {
            return $"Product description must stat with {_name}";
        }
    }
}
