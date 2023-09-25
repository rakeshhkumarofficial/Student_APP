using AutoMapper;
using BAL.IServices;
using DAL.IRepositories;
using DAL.Models;
using STUDENT_SHARED.DTOs;
using System.Net;


namespace BAL.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private ResponseDTO _response;

        public StudentService(IStudentRepository studentRepository, IAddressRepository addressRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _response = new ResponseDTO();
        }

        public async Task<ResponseDTO> GetAsync(string searchString , int index = 1 , int limit = 10)
        {
            try
            {
                var dataModel = await _studentRepository.GetAsync(searchString , index , limit);
                var studentReponseDTOs = new List<StudentReponseDTO>();

                foreach (var student in dataModel.Data)
                {
                    var addresses = await _addressRepository.GetByStudentIdAsync(student!.Id);
                    StudentReponseDTO studentResponseDTO = _mapper.Map<StudentReponseDTO>(student);
                    studentResponseDTO.Addresses = _mapper.Map<List<AddressResponseDTO>>(addresses);
                    studentReponseDTOs.Add(studentResponseDTO);
                }
                var dataModelDTO = new DataModelDTO()
                {
                    Data = studentReponseDTOs,
                    TotalCount = dataModel.TotalCount
                };
                _response.Message = "Student List";
                _response.Data = dataModelDTO;
                return _response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseDTO> CreateAsync(StudentDTO studentDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(studentDTO.Name))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Please enter the student Name";
                    return _response;
                }

                if (string.IsNullOrEmpty(studentDTO.Email))
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Please enter the valid email";
                    return _response;
                }

                if (studentDTO.DateOfBirth == DateTime.MinValue)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Please Enter Your date of birth";
                    return _response;
                }

                if (studentDTO.Addresses == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Please enter the address details";
                    return _response;
                }

                Student isExist = await _studentRepository.GetByEmailAsync(studentDTO.Email);
                if (isExist != null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Message = "Email Already Exists";
                    return _response;
                }

                Student student = _mapper.Map<Student>(studentDTO);
                student.CreatedAt = DateTime.Now;
                student.UpdatedAt = DateTime.Now;
                await _studentRepository.CreateAsync(student);

                foreach (var add in studentDTO.Addresses!)
                {
                    Address address = _mapper.Map<Address>(add);
                    address.StudentId = student.Id;
                    address.CreatedAt = DateTime.Now;
                    address.UpdatedAt = DateTime.Now;
                    await _addressRepository.CreateAsync(address);
                }

                var addresses = await _addressRepository.GetByStudentIdAsync(student!.Id);
                StudentReponseDTO studentResponseDTO = _mapper.Map<StudentReponseDTO>(student);
                studentResponseDTO.Addresses = _mapper.Map<List<AddressResponseDTO>>(addresses);
                
                _response.Message = "Student Created Successfully";
                _response.Data = studentResponseDTO;
                return _response;
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
                var student = await _studentRepository.GetByIdAsync(Id);
                if (student == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "Student Not Found";
                    return _response;
                }
                var addresses = await _addressRepository.GetByStudentIdAsync(Id);
                foreach (var add in addresses)
                {
                    await _addressRepository.DeleteAsync(add);
                }
                await _studentRepository.DeleteAsync(student);
                _response.Message = "Student Deleted";
                _response.Data = student;
                return _response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseDTO> GetByIdAsync(Guid Id)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(Id);
                if (student == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "Student Not Found";
                    return _response;
                }
                var addresses = await _addressRepository.GetByStudentIdAsync(student!.Id);

                StudentReponseDTO studentResponseDTO = _mapper.Map<StudentReponseDTO>(student);
                studentResponseDTO.Addresses = _mapper.Map<List<AddressResponseDTO>>(addresses);
                _response.Message = "Student Details";
                _response.Data = studentResponseDTO;
                return _response;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ResponseDTO> UpdateAsync(Guid Id, StudentUpdateDTO studentDTO)
        {
            try
            {
                var student = await _studentRepository.GetByIdAsync(Id);
                if (student == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.Message = "Student Not Found";
                    return _response;
                }

                if (!string.IsNullOrEmpty(studentDTO.Name))
                {
                    student.Name = studentDTO.Name;
                }

                if (!string.IsNullOrEmpty(studentDTO.Email))
                {
                    if (student.Email!.ToLower() != studentDTO.Email.ToLower())
                    {
                        var isStudentExist = await _studentRepository.GetByEmailAsync(studentDTO.Email);
                        if (isStudentExist != null)
                        {
                            _response.IsSuccess = false;
                            _response.StatusCode = HttpStatusCode.BadRequest;
                            _response.Message = "Email Already Exists";
                            return _response;
                        }
                        student.Email = studentDTO.Email;
                    }
                }

                student.DateOfBirth = studentDTO.DateOfBirth;
                student.Gender = studentDTO.Gender;
                student.IsEnglish = studentDTO.IsEnglish;
                student.IsHindi = studentDTO.IsHindi;
                student.ProfileImage = studentDTO.ProfileImage;
                
                student.UpdatedAt = DateTime.Now;
                await _studentRepository.UpdateAsync(student);

                if (studentDTO.Addresses != null)
                {
                    foreach (var add in studentDTO.Addresses)
                    {
                        Address address = await _addressRepository.GetByIdAsync(add.Id);
                        if (address != null)
                        {
                            address.State = add.State;
                            address.City = add.City;
                            address.Country = add.Country;
                            address.ZipCode = add.ZipCode;
                            address.UpdatedAt = DateTime.Now;
                            await _addressRepository.UpdateAsync(address);
                        }
                    }
                }

                var addresses = await _addressRepository.GetByStudentIdAsync(student!.Id);
                StudentReponseDTO studentResponseDTO = _mapper.Map<StudentReponseDTO>(student);
                studentResponseDTO.Addresses = _mapper.Map<List<AddressResponseDTO>>(addresses);
                _response.Message = "Student Details Updated";
                _response.Data = studentResponseDTO;
                return _response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
