using SunNxtBackend.Models;

namespace SunNxtBackend.ViewModels
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int StateId { get; set; }
        public string? StateName { get; set; }
    }
}
