using STUDENT_WEB.Services.Contracts;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using STUDENT_SHARED.DTOs;

namespace STUDENT_WEB.Services
{
    public class StudentContract : IStudentContract
    {
        private readonly HttpClient _httpClient;
        private string baseURL = "https://localhost:7060/api/student";

        public StudentContract(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTO> CreateAsync(StudentDTO studentDTO)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, baseURL);
                request.Content = new StringContent(JsonSerializer.Serialize(studentDTO), Encoding.UTF8, "application/json");
                var result = await _httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = await result.Content.ReadFromJsonAsync<ResponseDTO>();
                    return errorResponse!;
                }
                var response = await result.Content.ReadFromJsonAsync<ResponseDTO>();
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO> DeleteAsync(Guid Id)
        {
            try
            {
                var response = await _httpClient.DeleteFromJsonAsync<ResponseDTO>($"{baseURL}/{Id}");
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO> GetAsync(Guid Id)
        {
            try
            {
                string apiUrl = $"{baseURL}?";
                if (Id != Guid.Empty)
                {
                    apiUrl += $"id={Id}&";
                }
                var response = await _httpClient.GetFromJsonAsync<ResponseDTO>(apiUrl.TrimEnd('&'));
                Console.WriteLine(response);
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseDTO> UpdateAsync(Guid Id, StudentUpdateDTO studentUpdateDTO)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"{baseURL}/{Id}");
                request.Content = new StringContent(JsonSerializer.Serialize(studentUpdateDTO), Encoding.UTF8, "application/json");

                var result = await _httpClient.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                {
                    var errorResponse = await result.Content.ReadFromJsonAsync<ResponseDTO>();
                    return errorResponse!;
                }
                var response = await result.Content.ReadFromJsonAsync<ResponseDTO>();
                return response!;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
