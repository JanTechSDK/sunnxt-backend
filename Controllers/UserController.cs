using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IAppUserService appUserService,
                              ILogger<UserController> logger)
        {
            _appUserService = appUserService;
            _logger = logger;
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
