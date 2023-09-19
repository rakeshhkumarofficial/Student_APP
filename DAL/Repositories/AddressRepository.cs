using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Data;
using DAL.IRepositories;

namespace DAL.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AddressRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// create student address
        /// </summary>
        /// <param name="address"></param>
        public async Task CreateAsync(Address address)
        {
            try
            {
                await _dbContext.Address.AddAsync(address);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// delete student address 
        /// </summary>
        /// <param name="address"></param>
        public async Task DeleteAsync(Address address)
        {
            try
            {
                _dbContext.Address.Remove(address);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get student address by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Task{Address}</returns>
        public async Task<Address> GetByIdAsync(Guid Id)
        {
            Address? address = await _dbContext.Address.FirstOrDefaultAsync(x => x.Id == Id);
            return address!;
        }

        /// <summary>
        /// Get student addresses by student Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Task{List{Address}}</returns>
        public async Task<List<Address>> GetByStudentIdAsync(Guid Id)
        {
            try
            {
                List<Address>? addresses = await _dbContext.Address.Where(x=>x.StudentId==Id).ToListAsync();
                return addresses!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update student Address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Address address)
        {
            try
            {
                _dbContext.Address.Update(address);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
