using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record AddOutBoundAccessByNameDto
    {
        public string FromDCName { get; set; } = string.Empty;
        public string ToDCName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public int Port { get; set; }
        public DatabaseDirection Direction = DatabaseDirection.OutBound;
    }
}
