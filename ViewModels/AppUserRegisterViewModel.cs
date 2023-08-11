namespace SunNxtBackend.ViewModels
{
    public class AppUserRegisterViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int AgeRangeId { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
    }
}
