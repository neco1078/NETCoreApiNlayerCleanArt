using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)

                .NotNull().WithMessage("ürün ismi gereklidir")
            .NotEmpty().WithMessage("ürün ismi gereklidir")
            .Length(3, 10).WithMessage("ürün 3 -10 karaktera arasında olmalı");

            //price validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("ürün fiyatı 0 dan büyük olmalıdır");
            RuleFor(x => x.Stock)
               .InclusiveBetween(1,100).WithMessage("stok adedi 1-100 arasında olmalıdır");
        }
    }
}
