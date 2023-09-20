

namespace STUDENT_WEB.DTOs
{
    public class AddressResponseDTO
    {
        public Guid id { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public int zipCode { get; set; }
        public bool isPermanent { get; set; }
        public Guid studentId { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
