using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend.Repositories
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetAll();

        Task<AppUser> GetAppUserById(int appUserId);

        Task<bool> SaveAppUserAsync(AppUser appUser);
        Task<bool> CheckMobileNumberExists(string mobileNumber);
        Task<AppUser?> GetAppUserByMobileNumber(string mobileNumber);

    }

    public class AppUserRepository : IAppUserRepository
    {
        private ApplicationDbContext _dbContext;
        private ILogger<AppUserRepository> _logger;

        public AppUserRepository(ApplicationDbContext dbContext, ILogger<AppUserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<List<AppUser>> GetAll()
        {
            return await _dbContext.AppUsers.Include(a => a.AgeRange)
                                            .Include(a => a.Country)
                                            .Include(a => a.State)
                                            .Include(c => c.City)
                                            .AsNoTracking().ToListAsync();
        }

        public async Task<AppUser> GetAppUserById(int appUserId)
        {
            return await _dbContext.AppUsers.Include(a => a.AgeRange)
                                            .Include(a => a.Country)
                                            .Include(a => a.State)
                                            .Include(c => c.City)
                                            .AsNoTracking().FirstOrDefaultAsync(s => s.Id == appUserId);
        }

        public async Task<bool> SaveAppUserAsync(AppUser appUser)
        {
            try
            {
                _dbContext.AppUsers.Add(appUser);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return false;
        }

        public async Task<bool> CheckMobileNumberExists(string mobileNumber)
        {
            return await _dbContext.AppUsers.AnyAsync(s=>s.MobileNumber == mobileNumber);
        }

        public async Task<AppUser?> GetAppUserByMobileNumber(string mobileNumber)
        {
            return await _dbContext.AppUsers.Include(a=>a.AgeRange)
                                            .Include(a => a.Country)
                                            .Include(a => a.State)
                                            .Include (c => c.City)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(s => s.MobileNumber == mobileNumber);
        }
    }
}
