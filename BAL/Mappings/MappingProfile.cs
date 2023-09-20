using AutoMapper;
using BAL.DTOs;
using DAL.Models;

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
