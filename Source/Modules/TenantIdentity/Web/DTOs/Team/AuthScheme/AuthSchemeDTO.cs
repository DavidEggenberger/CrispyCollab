namespace Modules.IdentityModule.Web.DTOs
{
    public class AuthSchemeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Handler { get; set; }
        public OpenIdOptionsDTO OpenIdOptions { get; set; }
    }
}
