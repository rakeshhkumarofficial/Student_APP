
using System.Text.Json.Serialization;

namespace STUDENT_SHARED.DTOs
{
    public class AddressResponseDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("state")]
        public string? State { get; set; }
        [JsonPropertyName("country")]
        public string? Country { get; set; }
        [JsonPropertyName("zipcode")]
        public int ZipCode { get; set; }
        [JsonPropertyName("isPermanent")]
        public bool IsPermanent { get; set; }

        [JsonPropertyName("studentId")]
        public Guid StudentId { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
