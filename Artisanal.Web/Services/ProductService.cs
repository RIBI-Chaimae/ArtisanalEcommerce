using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using System;
using static Artisanal.Web.SD;

namespace Artisanal.Web.Services
{
    public class ProductService : BaseService   , IProductService 
    {private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            httpClientFactory = httpClientFactory;
        }

        public async Task<T> CreateProductsAsync<T>( ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.POST,
                data = productDto,
                url = SD.ProductApiBase + "/api/products",
               // AccessToken = token
            }
                 );
        }

        public async Task<T> DeleteProductsAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.DELETE,
                url = SD.ProductApiBase + "/api/products/" + id,
                // AccessToken = token
            }
                );
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,

                url = SD.ProductApiBase+ "/api/products",
                //AccessToken = token
            }
                ); 
        }

        public async Task<T> GetProductByIdAsync<T>( int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.GET,

                url = SD.ProductApiBase + "/api/products/"+id,
               // AccessToken = token
            }
                 );
        }

        public async Task<T> UpdateProductsAsync<T>( ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                apiType = SD.ApiType.PUT,
                data=productDto,

                url = SD.ProductApiBase + "/api/products",
               // AccessToken = token
            }
                ); throw new NotImplementedException();
        }
    }
}
