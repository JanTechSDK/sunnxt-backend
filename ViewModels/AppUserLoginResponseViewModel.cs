namespace SunNxtBackend.ViewModels
{
    public class AppUserLoginResponseViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; } = string.Empty;

        public int AgeRangeId { get; set; }
        public string? AgeRangeName { get; set; }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public int StateId { get; set; }
        public string? StateName { get; set; }

        public int CityId { get; set; }
        public string? CityName { get; set; }

        public string? Token { get; set;}
    }
}
