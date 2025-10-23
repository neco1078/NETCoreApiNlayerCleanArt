using App.Repositories;
using App.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public class ProductService(IProductRepository productRepository): IProductService
    {
        public async Task<ServiceResult<List<Product>>> GetToPriceProductsAsync(int count)
        {
            var products= await productRepository.GetTopPriceProductsAsync(count)
            return new ServiceResult<List<Product>>()
            {
                Data = products
            };
        }

        public async Task<ServiceResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
              ServiceResult<Product>.Fail("Product not found", System.Net.HttpStatusCode.NotFound); 
            }
          return ServiceResult<Product>.Succecss(product!);
        }
    }
}
