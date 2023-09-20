using BAL.DTOs;
using BAL.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace STUDENT_DEMO.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private ResponseDTO _response;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
            _response = new();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Post([FromBody] StudentDTO studentDTO)
        {
            try
            {
                 _response = await _studentService.CreateAsync(studentDTO);
                 return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> Get([FromQuery] Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    _response = await _studentService.GetByIdAsync(id);
                }
                else
                {
                    _response = await _studentService.GetAsync();
                }
                return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update Student By Id 
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ResponseDTO>> Put([FromRoute] Guid id, [FromBody] StudentUpdateDTO studentDTO)
        {
            try
            {
                _response = await _studentService.UpdateAsync(id, studentDTO);
                return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete Student By Id
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ResponseDTO>> Delete([FromRoute] Guid id)
        {
            try
            {
                _response = await _studentService.DeleteAsync(id);
                return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
