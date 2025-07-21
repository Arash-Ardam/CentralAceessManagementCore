using Domain.DataModels;
using Domain.Enums;

namespace CAM.Api.Dtos
{
    public record SearchAccessResultDto
    {
        public short Id { get; set; } = 0;
        public DatabaseEngine Source { get; set; } = DatabaseEngine.Empty;
        public DatabaseEngine Destination { get; set; } = DatabaseEngine.Empty;
        public int Port { get; set; } = 0;

        public DatabaseDirection Direction { get; set; } = default;
    }
}
