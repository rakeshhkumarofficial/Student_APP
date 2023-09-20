using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_WEB.DTOs
{
    public class StudentDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }
        public List<AddressDTO>? Addresses { get; set; } = new List<AddressDTO>();
    }
}
