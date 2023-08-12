using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SunNxtBackend.Repositories;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityRepository cityRepository,
                                IMapper mapper,
                                ILogger<CityController> logger)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            List<CityViewModel> lstCity = new();

            try
            {
                var cityList = await _cityRepository.GetAll();
                lstCity = _mapper.Map<List<CityViewModel>>(cityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstCity);
        }

        [HttpGet("GetAllCitiesByStateId/{stateId}")]
        public async Task<IActionResult> GetAllCitiesByStateId(int stateId)
        {
            List<CityViewModel> lstCity = new();

            try
            {
                var cityList = await _cityRepository.GetAllCitiesByStateId(stateId);
                lstCity = _mapper.Map<List<CityViewModel>>(cityList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstCity);
        }
    }
}
