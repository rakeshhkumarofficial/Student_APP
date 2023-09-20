using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create Student
        /// </summary>
        /// <param name="student"></param>
        public async Task CreateAsync(Student student)
        {
            try
            {
                await _dbContext.Student.AddAsync(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="student"></param>
        public async Task DeleteAsync(Student student)
        {
            try
            {
                _dbContext.Student.Remove(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Student By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task{Student}</returns>
        public async Task<Student> GetByEmailAsync(string email)
        {
            try
            {
                Student? student = await _dbContext.Student.FirstOrDefaultAsync(x => x.Email!.ToLower() == email.ToLower());
                return student!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get Student By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Task{Student}</returns>
        public async Task<Student> GetByIdAsync(Guid Id)
        {
            try
            {
                Student? student = await _dbContext.Student.FirstOrDefaultAsync(x=>x.Id==Id);
                return student!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get all students
        /// </summary>
        /// <returns></returns>
        public async Task<List<Student>> GetAsync()
        {
            try
            {
                List<Student>? students = await _dbContext.Student.ToListAsync();
                return students!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Student student)
        {
            try
            {
                _dbContext.Student.Update(student);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
