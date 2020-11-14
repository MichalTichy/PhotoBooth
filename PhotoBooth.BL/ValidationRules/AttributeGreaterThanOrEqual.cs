using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBooth.BL.ValidationRules
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class AttributeGreaterThanOrEqual : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public AttributeGreaterThanOrEqual(string comparisonProperty) { _comparisonProperty = comparisonProperty; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult("Invalid entry");
            ErrorMessage = ErrorMessageString;

            if (value.GetType() == typeof(IComparable)) throw new ArgumentException("value has not implemented IComparable interface");
            var currentValue = (IComparable)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null) throw new ArgumentException("Comparison property with this name not found");

            var comparisonValue = property.GetValue(validationContext.ObjectInstance);
            if (!ReferenceEquals(value.GetType(), comparisonValue.GetType()))
                throw new ArgumentException("The types of the fields to compare are not the same.");

            return currentValue.CompareTo((IComparable)comparisonValue) >= 0 ? ValidationResult.Success : new ValidationResult(ErrorMessage);

        }
    }
}