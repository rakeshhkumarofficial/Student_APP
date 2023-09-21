using System.Net;
using System.Text.Json.Serialization;

namespace STUDENT_SHARED.DTOs
{
    public class ResponseDTO
    {
        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; } = true;
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public object? Data { get; set; } = new object();
    }
}
