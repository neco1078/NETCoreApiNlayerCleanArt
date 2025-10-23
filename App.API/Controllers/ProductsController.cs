using App.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
 
    public class ProductsController(IProductService productService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = productService.GetAllListAsync();
           return CreateActionResult(await serviceResult);

        }
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber,int pageSize)
        {
            var serviceResult = productService.GetPagedAllListAsync(pageNumber,pageSize);
            return CreateActionResult(await serviceResult);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await productService.GetByIdAsync(id);

            return CreateActionResult( serviceResult);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var serviceResult = await productService.CreateAsync(request);
            return CreateActionResult(serviceResult);
        }

        [HttpPut("{id}")]//tam güncelleme
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            var serviceResult = await productService.UpdateAsync(id, request);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }

        [HttpPatch("stock")]//kısmı güncelleme
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        {
            return CreateActionResult(await productService.UpdateStockAsync(request));
        }

        //[HttpPut("UpdateStock")]//kısmı güncelleme
        //public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        //{
        //    return CreateActionResult(await productService.UpdateStockAsync(request));
        //}

    }
}
