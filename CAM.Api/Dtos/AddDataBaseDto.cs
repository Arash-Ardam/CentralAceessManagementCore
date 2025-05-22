namespace CAM.Api.Dtos
{
    public record AddDataBaseDto
    {
        public string DataCenterName { get; set; } = string.Empty;
        public string DataBaseEngineName { get; set; } = string.Empty;
        public string DataBaseName { get; set; } = string.Empty;
    }
}
