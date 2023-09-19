using STUDENT_WEB.DTOs;

namespace STUDENT_WEB.Services.Contracts
{
    public interface IStudentContract
    {
        Task<ResponseDTO> CreateAsync(StudentDTO studentDTO);
        Task<ResponseDTO> GetAsync(Guid Id);
        Task<ResponseDTO> UpdateAsync(StudentUpdateDTO studentUpdateDTO);
        Task<ResponseDTO> DeleteAsync(Guid Id);
    }
}
