using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace rr_library.Models.DataAnnotations
{
    public class MaxLengthForHashSetAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        public MaxLengthForHashSetAttribute(int minLength = 32, int maxLength = 32)
        {
            _maxLength = maxLength;
            _minLength = minLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is HashSet<string> hashSet)
            {
                foreach (var item in hashSet)
                {
                    if (item.Length > _maxLength)
                    {
                        return new ValidationResult($"Each string in the {validationContext.DisplayName} must be at most {_maxLength} characters long.");
                    }
                    if (item.Length < _minLength)
                    {
                        return new ValidationResult($"Each string in the {validationContext.DisplayName} must be at least {_minLength} characters long.");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
