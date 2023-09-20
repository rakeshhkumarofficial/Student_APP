using AutoMapper;
using DAL.Models;
using STUDENT_SHARED.DTOs;

namespace BAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<Student, StudentUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressUpdateDTO>().ReverseMap();
            CreateMap<Address, AddressResponseDTO>().ReverseMap();
        }
    }
}
