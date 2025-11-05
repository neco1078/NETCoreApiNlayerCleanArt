using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public interface ICategoryService
    {

        Task<ServiceResult<CategoryWithProductsDTO>> GetCategoryWithProductsAsync(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDTO>>> GetCategoryWithProductsAsync();

        Task<ServiceResult<List<CategoryDTO>>> GetAllListAsync();

        Task<ServiceResult<CategoryDTO>> GetByIdAsync(int id);
        Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
