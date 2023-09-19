namespace BAL.DTOs
{
    public class AddressDTO
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int ZipCode { get; set; }
        public bool IsPermanent { get; set; } = false;
    }
}
