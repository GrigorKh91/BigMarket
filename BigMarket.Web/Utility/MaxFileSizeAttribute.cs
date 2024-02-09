using System.ComponentModel.DataAnnotations;

namespace BigMarket.Web.Utility
{
    public sealed class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxfileSize;
        public MaxFileSizeAttribute(int maxfileSize)
        {
            _maxfileSize = maxfileSize;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (file.Length > (_maxfileSize * 1024 * 1024))
                {
                    return new ValidationResult($"Maximum allowed file size is {_maxfileSize} MB.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
