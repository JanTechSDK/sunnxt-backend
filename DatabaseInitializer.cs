using Microsoft.EntityFrameworkCore;
using SunNxtBackend.Models;

namespace SunNxtBackend
{
    public interface IDatabaseInitializer
    {
        Task Initialize();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private ApplicationDbContext _dbContext;

        public DatabaseInitializer(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Initialize()
        {
            await _dbContext.Database.MigrateAsync();

            if (!_dbContext.AgeRanges.Any())
            {
                var ageRangeList = new List<AgeRange>
                {
                    new AgeRange { AgeRangeName = "Lessthan 18" },
                    new AgeRange { AgeRangeName = "18+ to 30" },
                    new AgeRange { AgeRangeName = "30+ to 40" },
                    new AgeRange { AgeRangeName = "40+ to 50" },
                    new AgeRange { AgeRangeName = "50+ to 60" },
                    new AgeRange { AgeRangeName = "60 and above" }
                };

                await _dbContext.AgeRanges.AddRangeAsync(ageRangeList);
                await _dbContext.SaveChangesAsync();
            }

            bool countryAdded = false;
            if (!_dbContext.Countries.Any())
            {
                var countryList = new List<Country>
                {
                    new Country { CountryName = "India" },
                    new Country { CountryName = "Austrilia" }
                };

                await _dbContext.Countries.AddRangeAsync(countryList);
                await _dbContext.SaveChangesAsync();
                countryAdded = true;
            }


            bool stateAdded = false;
            if (!_dbContext.States.Any() && countryAdded)
            {
                var indiaCountry = await _dbContext.Countries.FirstOrDefaultAsync(s => s.CountryName.Equals("India"));
                if (indiaCountry != null)
                {
                    var stateList = new List<State>
                    {
                        new State { StateName = "TamilNadu", CountryId = indiaCountry.Id },
                        new State { StateName = "Kerala", CountryId = indiaCountry.Id }
                    };

                    await _dbContext.States.AddRangeAsync(stateList);
                    await _dbContext.SaveChangesAsync();
                    stateAdded = true;
                }
            }


            if (!_dbContext.Cities.Any() && stateAdded)
            {
                var tamilnaduState = await _dbContext.States.FirstOrDefaultAsync(s => s.StateName.Equals("TamilNadu"));
                if (tamilnaduState != null)
                {
                    var cityMasterList = new List<City>
                    {
                        new City { CityName = "Chennai", StateId = tamilnaduState.Id },
                        new City { CityName = "Kallakurichi", StateId = tamilnaduState.Id },
                        new City { CityName = "Villupuram", StateId = tamilnaduState.Id },
                        new City { CityName = "Trichy", StateId = tamilnaduState.Id }
                    };

                    await _dbContext.Cities.AddRangeAsync(cityMasterList);
                    await _dbContext.SaveChangesAsync();
                }
            }

        }
    }
}
