using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace STUDENT_WEB.DTOs
{
    public class ResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
