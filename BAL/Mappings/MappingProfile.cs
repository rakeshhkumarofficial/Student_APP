using AutoMapper;
using DAL.Models;
using STUDENT_SHARED.DTOs;

namespace BAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();
            CreateMap<Student, StudentReponseDTO>().ReverseMap();

            CreateMap<Address, AddressDTO>().ReverseMap();  
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressResponseDTO>().ReverseMap();
        }
    }
}
