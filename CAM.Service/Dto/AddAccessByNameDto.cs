using Domain.Enums;

namespace CAM.Service.Dtos
{
    public record AddAccessByNameDto
    {
        public string DCName { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string ToName { get; set; } = string.Empty;
        public int Port { get; set; }

        public DatabaseDirection Direction { get; set; }
    }
}
