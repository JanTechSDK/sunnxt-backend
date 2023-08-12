using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SunNxtBackend.Repositories;
using SunNxtBackend.Services;
using SunNxtBackend.ViewModels;

namespace SunNxtBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IAppUserService appUserService,
                              IAppUserRepository appUserRepository,
                              IMapper mapper,
                              ILogger<UserController> logger)
        {
            _appUserService = appUserService;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AppUserViewModel> lstUser = new();

            try
            {
                var userList = await _appUserRepository.GetAll();
                lstUser = _mapper.Map<List<AppUserViewModel>>(userList);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return Ok(lstUser);
        }

        [HttpGet("GetAppUserById/{appUserId}")]
        public async Task<IActionResult> GetAppUserById(int appUserId)
        {
            AppUserViewModel lstUser = new();

            try
            {
                var userList = await _appUserRepository.GetAppUserById(appUserId);
                lstUser = _mapper.Map<AppUserViewModel>(userList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return Ok(lstUser);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(AppUserRegisterViewModel appUserRegisterViewModel)
        {
            var result = await _appUserService.SaveAppUserAsync(appUserRegisterViewModel);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AppUserLoginViewModel appUserLoginViewModel)
        {
            var result = await _appUserService.LoginAsync(appUserLoginViewModel);
            return Ok(result);
        }

    }
}
