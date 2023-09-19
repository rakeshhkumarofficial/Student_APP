namespace STUDENT_WEB.DTOs
{
    public class AddressUpdateDTO
    {
        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int ZipCode { get; set; }
        public bool IsPermanent { get; set; } = false;
    }
}
