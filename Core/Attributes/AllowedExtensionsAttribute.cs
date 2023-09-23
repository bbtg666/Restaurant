using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static Core.Constants.Constants;

namespace Restaurant.Helper.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        private readonly string _errorMessage;
        public AllowedExtensionsAttribute(string[] extensions, string errorMessage)
        {
            _extensions = extensions;
            _errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(_errorMessage);
                }
            }

            return ValidationResult.Success;
        }
    }
}
