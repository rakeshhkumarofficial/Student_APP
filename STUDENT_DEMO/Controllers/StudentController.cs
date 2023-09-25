using BAL.IServices;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STUDENT_SHARED.DTOs;
using System.Net;

namespace STUDENT_API.Controllers
{
    /// <summary>
    /// Controller for managing student data.
    /// </summary>
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

        /// <summary>
        /// Creates a new student record.
        /// </summary>
        /// <param name="studentDTO">The student data to create.</param>
        /// <returns>A response indicating success or failure.</returns>
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
                return StatusCode(500, "Internal Server Error");
            }
        }


        /// <summary>
        /// Retrieves a student record by ID or all students if ID is not provided.
        /// </summary>
        /// <param name="id">The ID of the student to retrieve (optional).</param>
        /// <returns>A response containing student data.</returns>
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> Get([FromQuery] Guid id , string searchString = "", int index = 1 , int limit = 10)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    _response = await _studentService.GetByIdAsync(id);
                }
                else
                {
                    _response = await _studentService.GetAsync(searchString, index , limit);
                }
                return _response;
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates a student record by ID.
        /// </summary>
        /// <param name="id">The ID of the student to update.</param>
        /// <param name="studentDTO">The updated student data.</param>
        /// <returns>A response indicating success or failure.</returns>
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
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Deletes a student record by ID.
        /// </summary>
        /// <param name="id">The ID of the student to delete.</param>
        /// <returns>A response indicating success or failure.</returns>
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
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
