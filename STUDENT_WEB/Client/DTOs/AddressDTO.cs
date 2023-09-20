using System.ComponentModel.DataAnnotations;

namespace STUDENT_WEB.DTOs
{
    public class AddressDTO
    {
        [Required(ErrorMessage = "City is required.")]
        public string? City { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [MaxLength(6)]
        public int ZipCode { get; set; }
        public bool IsPermanent { get; set; } = false;
    }
}
