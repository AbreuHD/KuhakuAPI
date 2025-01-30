namespace Core.Application.DTOs.General
{
    public class GenericApiResponse<DTO>
    {
        public required DTO Payload { get; set; }
        public required bool Success { get; set; } = true;
        public required int Statuscode { get; set; }
        public required string Message { get; set; }
    }
}
