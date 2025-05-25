using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record AddOutBoundAccessByAddressDto
    {
        public string FromDCName { get; set; } = string.Empty;
        public string ToDCName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public int Port { get; set; }

        public DatabaseDirection Direction = DatabaseDirection.OutBound;
    }
}
