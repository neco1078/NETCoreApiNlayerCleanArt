using App.Repositories;
using App.Repositories.Products;
using App.Services.ExceptionHandlers;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
       // IValidator<CreateProductRequest> createProductRequestValidator,
        IMapper mapper): IProductService
    {
        public async Task<ServiceResult<List<ProductDTO>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);
            //manuel mapper
           // var productAsDto = products.Select(p => new ProductDTO(p.Id, p.Name, p.Price, p.Stock)).ToList();
            var productAsDto = mapper.Map<List<ProductDTO>>(products);
            return ServiceResult<List<ProductDTO>>.Succecss(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDTO>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync() ;
            //manuel mapping
           // var productAsDto = products.Select(p => new ProductDTO(p.Id, p.Name, p.Price, p.Stock)).ToList();
            
            var productAsDto=mapper.Map<List<ProductDTO>>(products) ;
            return ServiceResult<List<ProductDTO>>.Succecss(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDTO>>> GetPagedAllListAsync(int pageNumber, int pageSize)
        {
            var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            //manuel mapping
            // var productAsDto = products.Select(p => new ProductDTO(p.Id, p.Name, p.Price, p.Stock)).ToList();

            var productAsDto = mapper.Map<List<ProductDTO>>(products);
            return ServiceResult<List<ProductDTO>>.Succecss(productAsDto);
        }

        public async Task<ServiceResult<ProductDTO?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
             return ServiceResult<ProductDTO?>.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
                
            }
            //var productAsDto = new ProductDTO(product!.Id,product.Name,product.Price,product.Stock);
            var productAsDto = mapper.Map<ProductDTO>(product);

            return ServiceResult<ProductDTO>.Succecss(productAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
           // throw new CriticalException("kritik seviye bir hata meydana geldi");
            
            //2.yol async service business check
            var anyProducts=await productRepository.Where(x=>x.Name==request.Name).AnyAsync();
            if (anyProducts)
            {
                return ServiceResult<CreateProductResponse>.Fail("ürün ismi DB de bulunmaktadır",HttpStatusCode.BadRequest);
            }
            //3.yol manuel async fluent validation businness check
            //var validationResult = await CreateProductRequestValidator.ValidateAsync(request);

            //if (validationResult.isValid) {

            //    return ServiceResult<CreateProductResponse>.Fail(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            //}

            //request productsa dönüştür.nesneolmadıgı için generic verdim
            var newProduct = mapper.Map<Product>(request);
            await productRepository.AddAsync(newProduct);
            await unitOfWork.SaveChangesAsync();
          
            return ServiceResult<CreateProductResponse>.SuccecssAsCreated(new CreateProductResponse(newProduct.Id), $"api/products/{newProduct.Id}");

        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request)
        {
            //Fast Fail
            //Guard Clause

            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
            }

            var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id!=product.Id).AnyAsync();
            if (isProductNameExist)
            {
                return ServiceResult.Fail("ürün ismi DB de bulunmaktadır", HttpStatusCode.BadRequest);
            }
            //product.Name = request.Name;
            //product.Price = request.Price;
            //product.Stock = request.Stock;

            //product nesnesi zaten vardı
            product=mapper.Map(request,product);
            productRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Succecss(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", System.Net.HttpStatusCode.NotFound);
            }
            productRepository.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Succecss(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request)
        {
            var product=await productRepository.GetByIdAsync(request.ProductId);
            if (product is null)
            {
                
                    return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
                

               
            }
            product.Stock = request.Quantity;
            productRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Succecss(HttpStatusCode.NoContent);

        }
    }
}
