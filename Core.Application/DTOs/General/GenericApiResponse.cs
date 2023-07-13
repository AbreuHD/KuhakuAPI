
using System.Text.Json;

namespace Core.Application.DTOs.General
{
    public class GenericApiResponse<DTO>
    {
        public DTO? Payload { get; set; }
        public bool Success { get; set; } = true;
        public int Statuscode { get; set; }
        public List<string>? Messages { get; set; } = new List<string> { };

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
