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

        [HttpGet]
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

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateProductRequest request)
        {
            var serviceResult = await productService.UpdateAsync(id, request);
            return CreateActionResult(serviceResult);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);
            return CreateActionResult(serviceResult);
        }
    }
}
