using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record SearchAccessDto
    {
        public string SourceDCName { get; set; } = string.Empty;
        public string DestinationDCName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public int Port { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}
