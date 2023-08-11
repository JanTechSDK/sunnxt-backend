namespace SunNxtBackend.Models
{
    public class AgeRange
    {
        public int Id { get; set; }
        public string AgeRangeName { get; set; } = string.Empty;

        public virtual ICollection<AppUser>? AppUsers { get; set; }
    }
}
