using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend.Repositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAll();
    }

    public class CountryRepository : ICountryRepository
    {
        private ApplicationDbContext _dbContext;

        public CountryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Country>> GetAll()
        {
            return await _dbContext.Countries.AsNoTracking().ToListAsync();
        }

    }
}
