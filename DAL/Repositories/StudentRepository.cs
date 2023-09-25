using Microsoft.EntityFrameworkCore;
using DAL.Data;
using DAL.Models;
using DAL.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using Azure;

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
                // await _dbContext.Student.AddAsync(student);
                // await _dbContext.SaveChangesAsync();
                student.Id = Guid.NewGuid();
                string Id = student.Id.ToString();
                student.ProfileImage = "hsfs";
                string dob = student.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss");
                string created = student.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                string updated = student.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                string StoredProc = $"EXEC CreateStudent @Id='{Id}', @Name = '{student.Name}', @Email = '{student.Email}', @Gender = {student.Gender} ,@DateOfBirth = '{dob}', @IsHindi={student.IsHindi}, @IsEnglish = {student.IsEnglish}, @ProfileImage ={student.ProfileImage}, @CreatedAt='{created}', @UpdatedAt ='{updated}'";
            
                
                await _dbContext.Database.ExecuteSqlRawAsync(StoredProc);



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
                //var student = await _dbContext.Student.FirstOrDefaultAsync(x=>x.Id==Id);
                
                string StoredProc = "EXEC GetStudentById @Id";
                var student = await _dbContext.Student.FromSqlRaw(StoredProc, new SqlParameter("@Id", Id)).ToListAsync();
                return student[0];

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
        public async Task<DataModel> GetAsync(string searchString, int index = 1, int limit = 10)
        {
            try
            {
                //IQueryable<Student> query = _dbContext.Student;
                //var totalItemCount = await query.CountAsync();
                //if (!string.IsNullOrEmpty(searchString))
                //{
                //    query = query.Where(s => s.Name.Contains(searchString) || s.Email.Contains(searchString));
                //}
                //query = query.Skip((index - 1) * limit).Take(limit);
                //var students = await query.ToListAsync();
                //var dataModel = new DataModel()
                //{
                //    Data = students,
                //    TotalCount = totalItemCount,
                //};
                //return dataModel!;

                SqlParameter totalCountParam = new SqlParameter("@TotalCount", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                string StoredProc = "EXEC GetStudents @searchString, @index, @limit, @TotalCount OUTPUT";
                var students = await _dbContext.Student
                    .FromSqlRaw(StoredProc,
                        new SqlParameter("@searchString", searchString ?? ""),
                        new SqlParameter("@index", index),
                        new SqlParameter("@limit", limit),
                        totalCountParam)
                    .ToListAsync();

                int totalItemCount = (int)totalCountParam.Value;

                var dataModel = new DataModel()
                {
                    Data = students,
                    TotalCount = totalItemCount
                };

                return dataModel;
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
