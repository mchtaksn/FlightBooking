using System.Text.Json.Serialization;

namespace FlightBooking.Models
{
    // list[] içindeki her bir havalimanı
    public class AirportItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }                  // örn "COV" -> IA

        [JsonPropertyName("type")]
        public string? Type { get; set; }                // "airport"

        [JsonPropertyName("title")]
        public string? Title { get; set; }               // "Çukurova Intern

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("distance")]
        public string? Distance { get; set; }
    }
}
