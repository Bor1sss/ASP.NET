using System.ComponentModel.DataAnnotations;

namespace MVC_first.Annotations
{
    public class FileCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {

                if (value != null && !(value.ToString().Contains(".jpg")))
                {
                    Console.WriteLine(value.ToString());
                    return new ValidationResult(ErrorMessage);
                }

            }
            return ValidationResult.Success!;
        }
    }
}
