namespace STUDENT_WEB.DTOs
{
    public class StudentUpdateDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public List<AddressUpdateDTO>? Addresses { get; set; }
    }
}
