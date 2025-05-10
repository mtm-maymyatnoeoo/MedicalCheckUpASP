using System.ComponentModel.DataAnnotations;

namespace MedicalCheckUpASP.Common
{
    // Custom validation to compare two properties if one is not null
    public class CompareIfPasswordNotNullAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public CompareIfPasswordNotNullAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var passwordProperty = validationContext.ObjectType.GetProperty(_otherProperty);
            if (passwordProperty == null)
            {
                return new ValidationResult($"Unknown property: {_otherProperty}");
            }

            var passwordValue = passwordProperty.GetValue(validationContext.ObjectInstance, null);

            // Only compare if the password is not null
            if (passwordValue != null && !passwordValue.Equals(value))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

    }

}
