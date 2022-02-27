using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System;

namespace WebWasmClient.Services
{
    public class HttpClientService
    {
        private HttpClient httpClient;
        public HttpClientService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("authorizedClient");
            httpClient.BaseAddress = new Uri(httpClient.BaseAddress.ToString());
        }
        public async Task<T> GetFromAPI<T>(string route)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api" + route);
            if(httpResponseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return default;
            }
            if(httpResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return default;
        }
    }
}
