﻿using System.ComponentModel.DataAnnotations;

namespace STUDENT_SHARED.DTOs
{
    public class StudentDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsHindi { get; set; } 
        public bool IsEnglish { get; set; }
        public string ProfileImage { get; set; }
        public List<AddressDTO>? Addresses { get; set; } = new List<AddressDTO>();
    }
}
