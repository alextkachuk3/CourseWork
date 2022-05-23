using System.Text.Json.Serialization;

namespace BookingWebService.Models
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Login { get; set; } = string.Empty;

        [JsonIgnore]
        public byte[]? PasswordHash { get; set; }

        [JsonIgnore]
        public byte[]? PasswordSalt { get; set; }

        [JsonIgnore]
        public List<Hotel> Hotels { get; set; } = new List<Hotel>();

    }
}
