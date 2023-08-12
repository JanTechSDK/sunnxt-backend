using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend.Repositories
{
    public interface IStateRepository
    {
        Task<List<State>> GetAll();
        Task<List<State>> GetAllStatesByCountryId(int countryId);
    }
    public class StateRepository : IStateRepository
    {
        private ApplicationDbContext _dbContext;

        public StateRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<State>> GetAll()
        {
            return await _dbContext.States.Include(c => c.Country).AsNoTracking().ToListAsync();
        }

        public async Task<List<State>> GetAllStatesByCountryId(int countryId)
        {
            return await _dbContext.States.Where(c => c.CountryId == countryId).Include(c => c.Country).AsNoTracking().ToListAsync();
        }
    }
}
