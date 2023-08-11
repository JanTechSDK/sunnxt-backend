using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SunNxtBackend.Repositories;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ICountryRepository countryRepository,
                                    IMapper mapper,
                                    ILogger<CountryController> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CountryViewModel> lstCountry = new();

            try
            {
                var countryList = await _countryRepository.GetAll();
                lstCountry = _mapper.Map<List<CountryViewModel>>(countryList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstCountry);
        }
    }
}
