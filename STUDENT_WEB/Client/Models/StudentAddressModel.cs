using STUDENT_SHARED.DTOs;
using System.ComponentModel.DataAnnotations;

namespace STUDENT_WEB.Models
{
    public class StudentAddressModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        public int Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid Date of Birth.")]
        public DateTime DateOfBirth { get; set; }
        public bool IsHindi { get; set; }
        public bool IsEnglish { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string? CurrentCity { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string? CurrentState { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? CurrentCountry { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        public int CurrentZipCode { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public string? PermanentCity { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public string? PermanentState { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? PermanentCountry { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        public int PermanentZipCode { get; set; }
    }
}
