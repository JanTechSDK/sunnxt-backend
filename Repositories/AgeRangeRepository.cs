using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend.Repositories
{
    public interface IAgeRangeRepository
    {
        Task<List<AgeRange>> GetAll();
    }

    public class AgeRangeRepository : IAgeRangeRepository
    {
        private ApplicationDbContext _dbContext;

        public AgeRangeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AgeRange>> GetAll()
        {
            return await _dbContext.AgeRanges.AsNoTracking().ToListAsync();
        }
    }
}
