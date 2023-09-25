using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STUDENT_SHARED.DTOs
{
    public class DataModelDTO
    {
        [JsonPropertyName("data")]
        public List<StudentReponseDTO> Data { get; set; }
        [JsonPropertyName("totalCount")]
        public int TotalCount  { get; set; }    
    }
}
