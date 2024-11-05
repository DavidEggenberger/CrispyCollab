namespace Modules.IdentityModule.Shared
{
    public class OpenIdOptionsDTO
    {
        public Guid Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Authority { get; set; }
        public string ResponseType { get; set; }
    }
}
