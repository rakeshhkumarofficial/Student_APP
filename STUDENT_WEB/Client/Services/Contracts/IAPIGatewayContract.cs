using STUDENT_SHARED.DTOs;
using STUDENT_WEB.Models;

namespace STUDENT_WEB.Services.Contracts
{
    public interface IAPIGatewayContract
    {
        Task<List<CountryResponse>> GetCountryNameAsync();
    }
}
