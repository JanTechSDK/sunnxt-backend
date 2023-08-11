namespace SunNxtBackend.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string MobileNumber { get; set; }
        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Gender { get; set; } = string.Empty;
        public int AgeRangeId { get; set; }
        public AgeRange? AgeRange { get; set; }

        public int CountryId { get; set; }
        public Country? Country { get; set; }

        public int StateId { get; set; }
        public State? State { get; set; }

        public int CityId { get; set; }
        public City? City { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
