using BAL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IServices
{
    public interface IStudentService
    {
        Task<ResponseDTO> CreateAsync(StudentDTO studentDTO);
        Task<ResponseDTO> GetByIdAsync(Guid Id);
        Task<ResponseDTO> UpdateAsync(StudentUpdateDTO studentDTO);
        Task<ResponseDTO> DeleteAsync(Guid Id);
    }
}
