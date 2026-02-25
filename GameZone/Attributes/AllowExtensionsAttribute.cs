using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class AllowExtensionsAttribute :ValidationAttribute
    {
        public readonly string _allowedExtensions;
        public AllowExtensionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           var file=value as IFormFile;
            if (file is not null)
            {
                var extension = Path.GetExtension(file.FileName);
                var isAllowed = _allowedExtensions.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);
                if (!isAllowed) 
                {
                    return new ValidationResult($"{_allowedExtensions} are allowed!");
                
                }
            }
            return ValidationResult.Success;
           
                
        }

    }
}
