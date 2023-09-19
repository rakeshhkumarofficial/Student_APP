using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int ZipCode { get; set; }
        public bool IsPermanent { get; set; } 
        public Guid StudentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
