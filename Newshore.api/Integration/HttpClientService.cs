using Newtonsoft.Json;

namespace Newshore.api.Integration
{
    public class HttpClientService<T>: IHttpClientService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string url = string.Empty;

        public HttpClientService(HttpClient httpClient, IConfiguration configuration) {
            _httpClient = httpClient;
            url = configuration["externalApi:routes"].ToString();
        }

        public async Task<List<T>> GetAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                List<T> result;
                string json = await response.Content.ReadAsStringAsync();
                 if(json is not null)
                {
                    result = JsonConvert.DeserializeObject<List<T>>(json);
                   return result;
                }

                throw new Exception($"Failed to retrieve data from {url}. Status code: {response.StatusCode}");
            }


            throw new Exception($"Failed to retrieve data from {url}. Status code: {response.StatusCode}");
        }

    }
}
