using SunNxtBackend.Models;

namespace SunNxtBackend.ViewModels
{
    public class StateViewModel
    {
        public int Id { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
