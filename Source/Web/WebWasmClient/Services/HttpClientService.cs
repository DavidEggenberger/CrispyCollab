using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

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
        public async Task<T> GetFromAPIAsync<T>(string route)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("/api" + route);
            if(httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch(Exception ex)
                {

                }
            }
            if(httpResponseMessage.IsSuccessStatusCode is false)
            {
                ProblemDetails problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new HttpClientServiceException(problemDetails.Detail);
            }
            return default;
        }

        public async Task<T> PostToAPIAsync<T>(string route, T t)
        {
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("/api" + route, t, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                try
                {
                    return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                catch (Exception ex)
                {

                }
            }
            if (httpResponseMessage.IsSuccessStatusCode is false)
            {
                ProblemDetails problemDetails = JsonSerializer.Deserialize<ProblemDetails>(await httpResponseMessage.Content.ReadAsStringAsync());
                throw new HttpClientServiceException(problemDetails.Detail);
            }
            return default;
        }
    }
    public class HttpClientServiceException : Exception
    {
        public HttpClientServiceException(string message) : base(message)
        {

        }
    }
}
