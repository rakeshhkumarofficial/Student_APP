using STUDENT_SHARED.DTOs;
namespace BAL.IServices
{
    public interface IStudentService
    {
        Task<ResponseDTO> CreateAsync(StudentDTO studentDTO);
        Task<ResponseDTO> GetByIdAsync(Guid Id);
        Task<ResponseDTO> GetAsync(string searchString, int index, int limit);
        Task<ResponseDTO> UpdateAsync(Guid Id, StudentUpdateDTO studentDTO);
        Task<ResponseDTO> DeleteAsync(Guid Id);
    }
}
