﻿using STUDENT_WEB.DTOs;

namespace STUDENT_WEB.Services.Contracts
{
    public interface IStudentContract
    {
        Task<ResponseDTO> CreateAsync(StudentDTO studentDTO);
        Task<ResponseDTO> GetAsync(Guid Id);
        Task<ResponseDTO> UpdateAsync(Guid Id , StudentUpdateDTO studentUpdateDTO);
        Task<ResponseDTO> DeleteAsync(Guid Id);
    }
}
