using Dependency_Injection.ServiceContracts;
using System.Text.Json;

namespace Dependency_Injection.Services
{
    public class FinHubService : IFinHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinHubService(IHttpClientFactory httpClientFactory , IConfiguration configuration) 
        { 
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // Implemented From IFinHubService.cs
        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient())
            {
                // string token = "cq3a8s1r01qobiisg640cq3a8s1r01qobiisg64g";
                
                // Create Request
                // Getting Token From User Secrets
                string token = _configuration["FinnhubToken"];
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={token}"),
                    Method = HttpMethod.Get
                };


                // Send Request
                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);


                // Receive Response
                Stream stream = httpResponseMessage.Content.ReadAsStream();
                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();
                

                // Covert response 
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if(responseDictionary == null)
                    throw new InvalidOperationException("No response from finhubbserver");

                if (responseDictionary.ContainsKey("error"))
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

                return responseDictionary;
            }
        }
    }
}
