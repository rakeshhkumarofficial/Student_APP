using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsHindi { get; set; } 
        public bool IsEnglish { get; set; }
        public string ProfileImage { get; set; } = string.Empty;
        public DateTime CreatedAt { get;set; }
        public DateTime UpdatedAt { get; set; }
    }
}
