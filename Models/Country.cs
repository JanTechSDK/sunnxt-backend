namespace SunNxtBackend.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public virtual ICollection<State>? States { get; }
        public virtual ICollection<AppUser>? AppUsers { get; set; }
    }
}
