﻿using Artisanal.Web.Models;
using Artisanal.Web.Services;
using Artisanal.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Artisanal.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;


        }
        public  async Task <IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            var response = await _productService.CreateProductsAsync<ResponseDto>(model);
            if (response != null&& response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
        public async Task<IActionResult> ProductEdit(int productId)
        {
            List<ProductDto> list = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto model)
    {
        var response = await _productService.UpdateProductsAsync<ResponseDto>(model);
        if (response != null && response.IsSuccess)
        {
            return RedirectToAction(nameof(ProductIndex));
        }
        return View(model);
    }
       
        public async Task<IActionResult> ProductDelete(int productId)
        {
            List<ProductDto> list = new();
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var response = await _productService.DeleteProductsAsync<ResponseDto>(model.ProductId);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
            return View(model);
        }
    }
}
