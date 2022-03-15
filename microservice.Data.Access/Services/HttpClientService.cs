using microservice.Core.IServices;
using microservice.Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microservice.Data.Access.Services
{
    public class HttpClientService : IHttpClientService
    {

        IHttpClientFactory _httpClientFactory;
        IConfiguration _configuration;

        public HttpClientService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task SendSMS(string phoneNumber, string message)
        {
            var httpClient = _httpClientFactory.CreateClient("localhost");

            var values = new
            {
                key = Constants.SMS4FREE_KEY,
                user = Constants.SMS4FREE_USER,
                pass = Constants.SMS4FREE_PASSWORD,
                sender = Constants.SMS4FREE_USER,
                recipient = phoneNumber,
                msg = $"BarberShop: Your confirmation code is: {message}."
            };


            var json = JsonConvert.SerializeObject(values);

            var context = new StringContent(json,Encoding.UTF8, "application/json");


            using (var response = await httpClient.PostAsync(_configuration.GetValue<string>(Constants.SMS4FREE_API), context))
            {
                //In the future I can handle the response by sending a message to the developers if something went wrong, for now I wont do that because this app is not built for production.

                var contents = await response.Content.ReadAsStringAsync();

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
