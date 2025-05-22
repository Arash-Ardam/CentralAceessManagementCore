using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record AddInBoundAccessByNameDto
    {
        public string DCName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public int Port { get; set; }
        public DatabaseDirection Direction = DatabaseDirection.InBound;
    }
}
