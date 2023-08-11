using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SunNxtBackend.Repositories;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgeRangeController : ControllerBase
    {
        private readonly IAgeRangeRepository _ageRangeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AgeRangeController> _logger;

        public AgeRangeController(IAgeRangeRepository ageRangeRepository,
                                    IMapper mapper,
                                    ILogger<AgeRangeController> logger) 
        { 
            _ageRangeRepository = ageRangeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AgeRangeViewModel> lstAgeRange = new();

            try
            {
                var ageRangeList = await _ageRangeRepository.GetAll();
                lstAgeRange = _mapper.Map<List<AgeRangeViewModel>>(ageRangeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstAgeRange);
        }
    }
}
