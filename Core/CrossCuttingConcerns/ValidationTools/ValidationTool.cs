using Microsoft.CodeAnalysis;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;

namespace Base.CrossCuttingConcerns.ValidationTools
{
    public static class ValidationTool
    {
        // bu tarz tool lar static yapılır. sebebi bir kere instance üretilir ve bellekte yer tutmaması için sürekli aynı instance çalıştırılır.
        public static void ValidateMethod(IValidator validator, Object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new FluentValidation.ValidationException(result.Errors);
            }
        }
    }
}
