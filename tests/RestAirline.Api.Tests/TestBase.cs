using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EventFlow.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RestAirline.Domain.EventSourcing;
using RestAirline.ReadModel.EntityFramework.DBContext;
using RestAirline.Shared.Extensions;

namespace RestAirline.Api.Tests
{
    public class TestBase : IDisposable
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        protected TestBase()
        {
            ApplicationBootstrap.AddTestingServicesRegistrar(r =>
            {
                r.RegisterServices(register =>
                {
                    register.Register<IDbContextProvider<EventStoreContext>, FakedEventStoreContextProvider>();
                    register.Register<IDbContextProvider<RestAirlineReadModelContext>, FakedEntityFramewokReadModelDbContextProvider>();
                });
            });

            var hostBuilder = new WebHostBuilder()
//                .UseContentRoot(WebProjectPath)
                .UseEnvironment("UnitTest")
                .UseStartup<Startup>();

            _server = new TestServer(hostBuilder);
            _client = _server.CreateClient();
        }

        protected IServiceProvider ServiceProvider => ApplicationBootstrap.ServiceProvider;


        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }

        protected async Task<TResponse> Get<TResponse>(string url)
        {
            var responseMessage = await _client.GetAsync(url);

            return await Deserialize<TResponse>(responseMessage);
        }

        protected async Task<TResponse> GetWithQuerystring<TRequest, TResponse>(string url, TRequest request)
        {
            var queryString = QuerystringGenerator.ToQueryString(request);

            return await Get<TResponse>(url + queryString);
        }

        protected async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest request)
        {
            var postData = new StringContent(request.Serialize(), Encoding.UTF8, "application/json");

            var responseMessage = await _client.PostAsync(url, postData);

            return await Deserialize<TResponse>(responseMessage);
        }

        protected async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest request)
        {
            var postData = new StringContent(request.Serialize(), Encoding.UTF8, "application/json");

            var responseMessage = await _client.PutAsync(url, postData);

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