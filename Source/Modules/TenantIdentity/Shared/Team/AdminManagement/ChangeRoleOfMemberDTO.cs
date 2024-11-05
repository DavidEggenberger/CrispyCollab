namespace Modules.IdentityModule.Shared
{
    public class ChangeRoleOfMemberDTO
    {
        public Guid UserId { get; set; }
        public TeamRoleDTO TargetRole { get; set; }
    }
}
