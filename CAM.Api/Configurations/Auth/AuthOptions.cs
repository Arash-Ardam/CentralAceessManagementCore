namespace CAM.Api.Configurations.Auth
{
    public class AuthOptions
    {
        public string Authority { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string RedirectUri { get; set; } = string.Empty;
        public bool HasPkce { get; set; } = false;
    }
}
