using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.ValidationRules
{
    public class InTheFuture : ValidationAttribute
    {
        public InTheFuture(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            if (dt >= DateTime.UtcNow)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}