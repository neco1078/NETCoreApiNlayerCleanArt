using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public interface IProductService
    {
        Task<ServiceResult<List<ProductDTO>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult<List<ProductDTO>>> GetAllListAsync();

        Task<ServiceResult<List<ProductDTO>>> GetPagedAllListAsync(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDTO?>> GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);

        Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> UpdateStockAsync(UpdateProductStockRequest request);
    }
}
