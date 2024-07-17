using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public FinnhubService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        // Function to get the company profile from the Finnhub API
        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri ($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };

            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);
            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
            Dictionary<string,object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return responseDictionary;
        }

        // Function to get the stock quote price from the Finnhub API
        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };

            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);
            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();
            Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

            return responseDictionary;
        }


    }
}
