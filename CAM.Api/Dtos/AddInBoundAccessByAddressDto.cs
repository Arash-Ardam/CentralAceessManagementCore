using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record AddInBoundAccessByAddressDto
    {
        public string DCName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string ToAddress { get; set; } = string.Empty;
        public int Port { get; set; }
        public DatabaseDirection Direction = DatabaseDirection.InBound;
    }
}
