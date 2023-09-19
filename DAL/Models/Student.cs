namespace DAL.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }   
        public DateTime CreatedAt { get;set; }
        public DateTime UpdatedAt { get; set; }
    }
}
