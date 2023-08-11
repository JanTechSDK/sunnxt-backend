namespace SunNxtBackend.Models
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int StateId { get; set; }
        public State? State { get; set; }

        public virtual ICollection<AppUser>? AppUsers { get; set; }
    }
}
