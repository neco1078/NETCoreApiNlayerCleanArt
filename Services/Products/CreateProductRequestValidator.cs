using App.Repositories.Products;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        private readonly IProductRepository _productRepository;



        public CreateProductRequestValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            RuleFor(x => x.Name)

                .NotNull().WithMessage("ürün ismi gereklidir")
            .NotEmpty().WithMessage("ürün ismi gereklidir")
            .Length(3, 10).WithMessage("ürün 3 -10 karaktera arasında olmalı");
            //.Must(MustUniqueProductName).WithMessage("ürün ismi veritabanında bulunmaktadır");
            //.MustAsync(MustUniqueProductNameAsync).WithMessage("ürün ismi veritabanında bulunmaktadır");
            
            //price validation
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("ürün fiyatı 0 dan büyük olmalıdır");
            //stock validation
            RuleFor(x => x.Stock)
               .InclusiveBetween(1, 100).WithMessage("stok adedi 1-100 arasında olmalıdır");
            _productRepository = productRepository;
        }
        //1.yol senkron validasyon
        // private bool MustUniqueProductName(string name)
        // {
        //     //false hata var
        //     //true hata yok

        //return !_productRepository.Where(x=> x.Name == name).Any();
        // }

        //3.yol  services.AddFluentValidationAutoValidation(); bu kapatılmalı
        //private async Task<bool> MustUniqueProductNameAsync(string productName,CancellationToken cancellationToken)
        //{
        //    return !await _productRepository.Where(x=>x.Name==productName).AnyAsync(cancellationToken);
        //}
    }
}
