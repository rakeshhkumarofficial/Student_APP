using DAL.Models;

namespace DAL.IRepositories
{
    public interface IAddressRepository
    {
        public Task CreateAsync(Address address);
        public Task UpdateAsync(Address address);
        public Task DeleteAsync(Address address);
        public Task<Address> GetByIdAsync(Guid Id);
        public Task<List<Address>> GetByStudentIdAsync(Guid Id);
    }
}
