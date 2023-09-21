using System.ComponentModel.DataAnnotations;

namespace STUDENT_SHARED.DTOs
{
    public class StudentUpdateDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsHindi { get; set; } 
        public bool IsEnglish { get; set; }
        public List<AddressUpdateDTO>? Addresses { get; set; } = new List<AddressUpdateDTO>();
    }
}
