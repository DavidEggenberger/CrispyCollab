namespace Modules.IdentityModule.Web.DTOs
{
    public class BFFUserInfoDTO
    {
        public static readonly BFFUserInfoDTO Anonymous = new BFFUserInfoDTO();
        public List<ClaimValueDTO> Claims { get; set; }
    }
}
