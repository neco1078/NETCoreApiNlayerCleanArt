using App.Repositories;
using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository,IUnitOfWork unitOfWork): IProductService
    {
        public async Task<ServiceResult<List<ProductDTO>>> GetTopPriceProductsAsync(int count)
        {
            var products = await productRepository.GetTopPriceProductsAsync(count);
            //manuel mapper
            var productAsDto = products.Select(p => new ProductDTO(p.Id, p.Name, p.Price, p.Stock)).ToList();
            return ServiceResult<List<ProductDTO>>.Succecss(productAsDto);
        }

        public async Task<ServiceResult<List<ProductDTO>>> GetAllListAsync()
        {
            var products = await productRepository.GetAll().ToListAsync() ;
            var productAsDto = products.Select(p => new ProductDTO(p.Id, p.Name, p.Price, p.Stock)).ToList();
            return ServiceResult<List<ProductDTO>>.Succecss(productAsDto);
        }

        public async Task<ServiceResult<ProductDTO?>> GetByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
              ServiceResult<ProductDTO>.Fail("Product not found", System.Net.HttpStatusCode.NotFound); 
            }
            var produstAsDto = new ProductDTO(product!.Id,product.Name,product.Price,product.Stock);
          return ServiceResult<ProductDTO>.Succecss(produstAsDto)!;
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
        {
            var newProduct = new Product()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await productRepository.AddAsync(newProduct);
            await unitOfWork.SaveChangesAsync();
          
            return ServiceResult<CreateProductResponse>.Succecss(new CreateProductResponse(newProduct.Id));

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
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;
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
    }
}
