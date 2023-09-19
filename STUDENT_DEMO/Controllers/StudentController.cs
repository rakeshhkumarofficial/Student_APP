using BAL.DTOs;
using BAL.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace STUDENT_DEMO.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<ResponseDTO>> Get([FromQuery] Guid Id)
        {
            try
            {
                _response = await _studentService.GetByIdAsync(Id);
                return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update Student By Id 
        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> Put([FromBody] StudentUpdateDTO studentDTO)
        {
            try
            {
                _response = await _studentService.UpdateAsync(studentDTO);
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
