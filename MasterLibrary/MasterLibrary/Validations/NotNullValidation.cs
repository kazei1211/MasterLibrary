using System.Globalization;
using System.Windows.Controls;

namespace MasterLibrary.Validations
{
    internal class NotNullValidation: ValidationRule
    {
        public string ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result = new ValidationResult(true, null);
            if (value == null)
                return result;
            if (value.ToString() == "")
            {
                return new ValidationResult(false, this.ErrorMessage);
            }
            return result;
        }
    }
}
