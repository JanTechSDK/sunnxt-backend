using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend.Repositories
{
    public interface ICityRepository
    {
        Task<List<City>> GetAll();
        Task<List<City>> GetAllCitiesByStateId(int stateId);
    }
    public class CityRepository : ICityRepository
    {
        private ApplicationDbContext _dbContext;
        public CityRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<City>> GetAll()
        {
            return await _dbContext.Cities.Include(s => s.State).AsNoTracking().ToListAsync();
        }

        public async Task<List<City>> GetAllCitiesByStateId(int stateId)
        {
            return await _dbContext.Cities.Where(s => s.StateId == stateId).Include(s => s.State).AsNoTracking().ToListAsync();
        }
    }
}
