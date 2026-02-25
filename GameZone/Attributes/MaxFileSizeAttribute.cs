using System.ComponentModel.DataAnnotations;

namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    {
        public readonly int _maxfilesize;
        public MaxFileSizeAttribute(int maxfilesize)
        {
            _maxfilesize = maxfilesize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
               
                if (file.Length>_maxfilesize)
                {
                    return new ValidationResult($"Maxmium Allowed Size is {_maxfilesize} bytes");

                }
            }
            return ValidationResult.Success;


        }
    }
}
