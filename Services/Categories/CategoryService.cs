using App.Repositories;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork,IMapper mapper):ICategoryService
    {

        // crud operation


        public async Task<ServiceResult<CategoryWithProductsDTO>> GetCategoryWithProductsAsync(int categoryId)
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category == null) { 
            return ServiceResult<CategoryWithProductsDTO>.Fail("kategori bulunamadı",HttpStatusCode.NotFound);
            }
            var categoryAsDto=mapper.Map<CategoryWithProductsDTO>(category);
            return ServiceResult<CategoryWithProductsDTO>.Succecss(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDTO>>> GetCategoryWithProductsAsync()
        {
            var category = await categoryRepository.GetCategoryWithProductsAsync().ToListAsync();
         
            var categoryAsDto = mapper.Map<List<CategoryWithProductsDTO>>(category);
            return ServiceResult<List<CategoryWithProductsDTO>>.Succecss(categoryAsDto);
        }
        public async Task<ServiceResult<List<CategoryDTO>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            var categoriesAsDto=mapper.Map<List<CategoryDTO>>(categories);
            return ServiceResult<List<CategoryDTO>>.Succecss(categoriesAsDto);
        }

        public async Task<ServiceResult<CategoryDTO>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult<CategoryDTO>.Fail("kategori bulunamadı",HttpStatusCode.NotFound);

            }
            var categoryAsDto= mapper.Map<CategoryDTO>(category);   
            return ServiceResult<CategoryDTO>.Succecss(categoryAsDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request) 
        {
            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("Kategori ismi DB de bulunmaktadır", HttpStatusCode.NotFound);
            }
            var newCategory = new Category { Name = request.Name };
           await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.Succecss(newCategory.Id);
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category =await categoryRepository.GetByIdAsync(id);

            if (category == null) { 
            return ServiceResult.Fail("güncellenecek kategori bulunamadı",HttpStatusCode.NotFound);
            
            }

            var isCategoryNameExist=await categoryRepository.Where(x=>x.Name == request.Name && x.Id!= category.Id).AnyAsync();
            if (isCategoryNameExist) { 
            return ServiceResult.Fail("kategori ismi DB de bulunmamaktadır",HttpStatusCode.BadRequest);
            }

            category=mapper.Map(request, category);
            categoryRepository.UpdateAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Succecss(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("kategori bulanmadı", HttpStatusCode.NotFound);
            }
            categoryRepository.DeleteAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Succecss(HttpStatusCode.NoContent);
        }
    }
}
