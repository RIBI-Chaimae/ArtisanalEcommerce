using Artisanal.Web.Models;
using Artisanal.Web.Services.IServices;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Artisanal.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("ArtisanaApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.data), Encoding.UTF8, "application/json");
                }
                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);


                }
                HttpResponseMessage apiResponse = null;
                switch (apiRequest.apiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post; break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put; break;
                    case SD.ApiType.GET:
                        message.Method = HttpMethod.Get; break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete; break;

                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessage = new List<string>
                    { Convert.ToString(e.Message)
                    },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }
        }

    }
}