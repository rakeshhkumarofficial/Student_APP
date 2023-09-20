using System.Net;

namespace STUDENT_SHARED.DTOs
{
    public class ResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
