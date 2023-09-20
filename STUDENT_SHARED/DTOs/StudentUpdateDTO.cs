namespace STUDENT_SHARED.DTOs
{
    public class StudentUpdateDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public List<AddressUpdateDTO>? Addresses { get; set; } = new List<AddressUpdateDTO>();
    }
}
