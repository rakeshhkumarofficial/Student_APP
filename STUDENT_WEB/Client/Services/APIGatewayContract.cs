using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Models;
using STUDENT_WEB.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace STUDENT_WEB.Services
{
    public class APIGatewayContract : IAPIGatewayContract
    {
        private readonly HttpClient _httpClient;
        private string CountryAPIURL = "http://api.countrylayer.com/v2/all";

        public APIGatewayContract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CountryResponse>> GetCountryNameAsync()
        {
            try
            {
                string apiUrl = $"{CountryAPIURL}?";
                string apikey = "a7024bf37f7e0731c1b9fa40d99828ef";


                apiUrl += $"access_key={apikey}&";
                
                var response = await _httpClient.GetFromJsonAsync<List<CountryResponse>>(apiUrl.TrimEnd('&'));
                Console.WriteLine(response);
                
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
