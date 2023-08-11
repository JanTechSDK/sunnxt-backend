namespace SunNxtBackend.Models
{
    public class State
    {
        public int Id { get; set; }
        public string StateName { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public Country? Country { get; set; } 

        public virtual ICollection<City>? Cities { get; }
        public virtual ICollection<AppUser>? AppUsers { get; set; }
    }
}
