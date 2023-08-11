using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SunNxtBackend.Repositories;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StateController> _logger;

        public StateController(IStateRepository stateRepository,
                                    IMapper mapper,
                                    ILogger<StateController> logger)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<StateViewModel> lstState = new();

            try
            {
                var stateList = await _stateRepository.GetAll();
                lstState = _mapper.Map<List<StateViewModel>>(stateList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstState);
        }

        [HttpGet("GetAllStatesByCountryId/{countryId}")]
        public async Task<IActionResult> GetAllStatesByCountryId(int countryId)
        {
            List<StateViewModel> lstState = new();

            try
            {
                var stateList = await _stateRepository.GetAllStatesByCountryId(countryId);
                lstState = _mapper.Map<List<StateViewModel>>(stateList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstState);
        }

    }
}
