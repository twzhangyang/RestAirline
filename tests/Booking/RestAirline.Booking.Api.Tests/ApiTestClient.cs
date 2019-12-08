using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestAirline.Shared.Extensions;

namespace RestAirline.Booking.Api.Tests
{
    public class ApiTestClient
    {
        private readonly HttpClient _httpClient;

        public ApiTestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            var responseMessage = await _httpClient.GetAsync(url);

            return await Deserialize<TResponse>(responseMessage);
        }

        public async Task<TResponse> GetWithQuerystring<TRequest, TResponse>(string url, TRequest request)
        {
            var queryString = QuerystringGenerator.ToQueryString(request);

            return await Get<TResponse>(url + queryString);
        }

        public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
        {
            var postData = new StringContent(request.Serialize(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PostAsync(url, postData);

            return await Deserialize<TResponse>(responseMessage);
        }

        public async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest request)
        {
            var postData = new StringContent(request.Serialize(), Encoding.UTF8, "application/json");

            var responseMessage = await _httpClient.PutAsync(url, postData);

            return await Deserialize<TResponse>(responseMessage);
        }

        private async Task<TResponse> Deserialize<TResponse>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();

            if (responseMessage.IsSuccessStatusCode)
            {
                return content.DeSerializeTo<TResponse>();
            }

            if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new ApiTestingBadRequestException(content);
            }

            throw new ApiTestingExecutionException(content);
        }
    }
}