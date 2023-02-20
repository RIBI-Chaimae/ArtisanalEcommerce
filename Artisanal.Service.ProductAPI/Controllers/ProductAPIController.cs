using Artisanal.Service.ProductAPI.Models.Dto;
using Artisanal.Service.ProductAPI.Repository;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artisanal.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private IProductRepository _productRepository;
        protected ResponseDto _response;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            IEnumerable<ProductDto> productDtos = await _productRepository.GetProducts();
            _response.Result = productDtos;
            return _response;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(id);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> Delete(int id)
        {
            try
            {
                 await _productRepository.DeleteProduct(id);

                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }
            return _response.IsSuccess;
        }
        [HttpPost]
        public async Task<object> Create([FromBody] ProductDto pdto)
        {
            try
            {
                ProductDto productDto = await _productRepository.CreateUpdateProduct(pdto);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
        [HttpPut]
        public async Task<object> Update([FromBody]ProductDto pdto)
        {
            try
            {
                ProductDto productDto = await _productRepository.CreateUpdateProduct(pdto);
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessage
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}