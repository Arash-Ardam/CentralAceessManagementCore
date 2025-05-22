namespace CAM.Api.Dtos
{
    public record AddAccessDto
    {
        public string DBName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
