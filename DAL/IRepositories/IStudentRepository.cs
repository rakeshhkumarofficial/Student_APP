using DAL.Models;

namespace DAL.IRepositories
{
    public interface IStudentRepository
    {
        public Task CreateAsync(Student student);
        public Task UpdateAsync(Student student);
        public Task DeleteAsync(Student student);
        public Task<Student> GetByIdAsync(Guid Id);
        public Task<Student> GetByEmailAsync(string Email);
    }
}
