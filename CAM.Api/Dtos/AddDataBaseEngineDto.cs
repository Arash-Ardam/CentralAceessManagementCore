﻿namespace CAM.Api.Dtos
{
    public record AddDataBaseEngineDto
    {
        public string dcName { get; set; } = string.Empty;
        public string dbEngineName { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
    }
}
