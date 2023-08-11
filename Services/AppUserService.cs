using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using SunNxtBackend.Models;
using SunNxtBackend.Repositories;
using SunNxtBackend.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SunNxtBackend.Services
{
    public interface IAppUserService
    {
        Task<ResultContentViewModel> SaveAppUserAsync(AppUserRegisterViewModel appUserRegisterViewModel);
        Task<ResultContentViewModel> LoginAsync(AppUserLoginViewModel appUserLoginViewModel);
    }

    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppUserService> _logger;
        public AppUserService(IAppUserRepository appUserRepository,
                                IMapper mapper,
                                ILogger<AppUserService> logger) 
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResultContentViewModel> SaveAppUserAsync(AppUserRegisterViewModel appUserRegisterViewModel)
        {
            try
            {
                if (appUserRegisterViewModel == null)
                {
                    return new ResultContentViewModel(false, "Please fill user details");
                }

                if (string.IsNullOrEmpty(appUserRegisterViewModel.MobileNumber))
                {
                    return new ResultContentViewModel(false, "Please fill MobileNumber");
                }

                var isMobileExist = await _appUserRepository.CheckMobileNumberExists(appUserRegisterViewModel.MobileNumber);

                if (isMobileExist)
                {
                    return new ResultContentViewModel(false, "MobileNumber already exists");
                }

                AppUser newAppUser = _mapper.Map<AppUser>(appUserRegisterViewModel);

                newAppUser.CreatedDate = DateTime.Now;

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(appUserRegisterViewModel.Password, out passwordHash, out passwordSalt);
                newAppUser.PasswordHash = passwordHash;
                newAppUser.PasswordSalt = passwordSalt;

                var result = await _appUserRepository.SaveAppUserAsync(newAppUser);

                if (result)
                {
                    return new ResultContentViewModel(true, "Successfully register");
                } else
                {
                    return new ResultContentViewModel(false, "unable register");
                }

            }
            catch(Exception ex )
            {
                _logger.LogError(ex.Message, ex);
            }

            return new ResultContentViewModel(false, "Something went wrong");
        }


        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash == null) throw new ArgumentNullException("Invalid length of password hash");


            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var comptedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < comptedHash.Length; i++)
                {
                    if (comptedHash[i] != storedHash[i]) return false;
                }
            }
            return true;

        }

        public async Task<ResultContentViewModel> LoginAsync(AppUserLoginViewModel appUserLoginViewModel)
        {
            try
            {

                if (appUserLoginViewModel == null)
                {
                    return new ResultContentViewModel(false, "Please fill user details");
                }

                if (string.IsNullOrEmpty(appUserLoginViewModel.MobileNumber))
                {
                    return new ResultContentViewModel(false, "Please fill MobileNumber");
                }

                if (string.IsNullOrEmpty(appUserLoginViewModel.Password))
                {
                    return new ResultContentViewModel(false, "Please fill Password");
                }

                var isMobileExist = await _appUserRepository.CheckMobileNumberExists(appUserLoginViewModel.MobileNumber);

                if (!isMobileExist)
                {
                    return new ResultContentViewModel(false, "MobileNumber not exists");
                }

                var existingUser = await _appUserRepository.GetAppUserByMobileNumber(appUserLoginViewModel.MobileNumber);

                if (existingUser == null)
                {
                    return new ResultContentViewModel(false, "User not exists");
                }

                var hasPasswordVerified = VerifyPasswordHash(appUserLoginViewModel.Password, existingUser.PasswordHash, existingUser.PasswordSalt);

                if (!hasPasswordVerified)
                {
                    return new ResultContentViewModel(false, "Invalid Password");
                }

                 // jwt token generation
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("my-secret-value1-my-secret-value2-my-secret-value3");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, existingUser.Id.ToString()),
                        new Claim(ClaimTypes.MobilePhone, existingUser.MobileNumber.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, existingUser.MobileNumber.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                var resp = _mapper.Map<AppUserLoginResponseViewModel>(existingUser);
                resp.Token = tokenString;

                return new ResultContentViewModel(true, resp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            return new ResultContentViewModel(false, "Something went wrong");
        }
    }
}
