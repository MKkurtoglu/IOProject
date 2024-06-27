using EntitiesLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    // ctrl k + d kodları düzeltir
    public class ProductValidator : AbstractValidator<Product> 
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(5).When(p => p.CategoryId == 1);

            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(1000).When(p => p.CategoryId == 1);


            //Olmayan kural yazımı

            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adları A harfi ile başlamalıdır.");
        }

        private bool StartWithA(string arg)
        {
            // arg yukarıdaki parametreden gelen p.productname'dir.
            return arg.StartsWith("A");
        }
    }
}
