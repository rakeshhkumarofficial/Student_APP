

namespace STUDENT_WEB.DTOs
{
    public class StudentReponseDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public int age { get; set; }
        public string? email { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public List<AddressResponseDTO> addresses { get; set; } = new List<AddressResponseDTO>();
    }
}
