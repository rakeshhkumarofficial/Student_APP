using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace STUDENT_SHARED.DTOs
{
    public class StudentReponseDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("dateofBirth")]
        public DateTime DateOfBirth { get; set; } 

        [JsonPropertyName("isHindi")]
        public bool IsHindi { get; set; }

        [JsonPropertyName("isEnglish")]
        public bool IsEnglish { get; set; }

        [JsonPropertyName("profileImage")]
        public string ProfileImage { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("addresses")]
        public List<AddressResponseDTO> Addresses { get; set; } = new List<AddressResponseDTO>();
    }
}
