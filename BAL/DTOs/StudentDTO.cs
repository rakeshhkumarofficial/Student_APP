using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs
{
    public class StudentDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public List<AddressDTO>? Addresses { get; set; } = new List<AddressDTO>();
    }
}
