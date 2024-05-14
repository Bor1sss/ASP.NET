using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MVC_first.Annotations
{
    public class DataCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime dateCheck = new DateTime(1900,1,1);
                if ((DateTime)value < dateCheck) return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success!;
        }
    }
}
