namespace Core.Application.DTOs.ShareList
{
    public class PreviewShareListDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Img { get; set; }
        public required string UserID { get; set; }
    }
}
