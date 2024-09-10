namespace Core.Application.DTOs.General
{
    public class GenericApiResponse<DTO>
    {
        public DTO Payload { get; set; }
        public bool Success { get; set; } = true;
        public int Statuscode { get; set; }
        public string Message { get; set; }
    }
}
