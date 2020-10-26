using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Basket.Services
{
    public class ApiHelper
    {
        private readonly HttpClient httpClient;
        public ApiHelper(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<T> Get<T>(Uri uri)
        {
            using (var response = await httpClient.GetAsync(uri))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
    }
}
