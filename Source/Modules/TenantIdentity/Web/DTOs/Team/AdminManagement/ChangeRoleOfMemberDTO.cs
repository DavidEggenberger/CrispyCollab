namespace Modules.IdentityModule.Web.DTOs
{
    public class ChangeRoleOfMemberDTO
    {
        public Guid UserId { get; set; }
        public TeamRoleDTO TargetRole { get; set; }
    }
}
