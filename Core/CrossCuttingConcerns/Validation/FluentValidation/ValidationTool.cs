using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public static class ValidationTool<T>
    {
        public static bool Validate(IValidator validator,T entity)
        {
            var validationContext = new ValidationContext<T>(entity);
            var result = validator.Validate(validationContext);

            return result.IsValid;
        }
    }
}
